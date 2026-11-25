import json
from pathlib import Path

import pytest

from rynekit_scraper.sources import solidjobs
from rynekit_scraper.sources.solidjobs import SolidJobsSource

PAGES = json.loads((Path(__file__).parent / "data" / "solidjobs.json").read_text("utf-8"))


class FakeResponse:
    def __init__(self, page: dict) -> None:
        self.page = page

    def raise_for_status(self) -> None:
        pass

    def json(self) -> dict:
        return self.page


@pytest.fixture
def calls(monkeypatch: pytest.MonkeyPatch) -> list[dict]:
    """Podmienia requests.get: zwraca stronę wg pageIndex i zapisuje, o co pytano."""
    calls = []

    def fake_get(url: str, params: dict, **kwargs: object) -> FakeResponse:
        calls.append(params)
        return FakeResponse(PAGES[params["pageIndex"]])

    monkeypatch.setattr(solidjobs.requests, "get", fake_get)
    monkeypatch.setattr(solidjobs.time, "sleep", lambda seconds: None)
    return calls


@pytest.fixture
def offers(calls: list[dict]) -> list:
    return list(SolidJobsSource(user_agent="test", campaign="test-campaign").fetch())


def test_offers_from_all_pages_without_missing_url(offers: list) -> None:
    assert [offer.source_offer_id for offer in offers] == [
        "aaaaaaaa-0000-0000-0000-000000000001",
        "aaaaaaaa-0000-0000-0000-000000000003",
    ]


def test_stops_after_last_page(offers: list, calls: list[dict]) -> None:
    assert [params["pageIndex"] for params in calls] == [0, 1]


def test_sends_campaign(offers: list, calls: list[dict]) -> None:
    assert all(params["campaign"] == "test-campaign" for params in calls)


def test_offer_has_source_and_link(offers: list) -> None:
    offer = offers[0]
    assert offer.source == "solidjobs"
    assert offer.source_url == "https://solid.jobs/o/acme0001/rynekit-uken"


def test_raw_json_keeps_whole_offer_unchanged(offers: list) -> None:
    assert json.loads(offers[1].raw_json) == PAGES[1]["jobs"][0]