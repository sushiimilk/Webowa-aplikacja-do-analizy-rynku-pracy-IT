import json
from pathlib import Path

import pytest

from rynekit_scraper.sources import remoteok
from rynekit_scraper.sources.remoteok import RemoteOkSource

SAMPLE = json.loads((Path(__file__).parent / "data" / "remoteok.json").read_text("utf-8"))


class FakeResponse:
    def raise_for_status(self) -> None:
        pass

    def json(self) -> list[dict]:
        return SAMPLE


@pytest.fixture
def offers(monkeypatch: pytest.MonkeyPatch) -> list:
    """Podmienia requests.get na zapisaną odpowiedź – test nie łączy się z internetem."""
    monkeypatch.setattr(remoteok.requests, "get", lambda *args, **kwargs: FakeResponse())
    return list(RemoteOkSource(user_agent="test").fetch())


def test_legal_notice_is_skipped(offers: list) -> None:
    assert [offer.source_offer_id for offer in offers] == ["123", "456"]


def test_offer_has_source_and_link(offers: list) -> None:
    offer = offers[0]
    assert offer.source == "remoteok"
    assert offer.source_url == "https://remoteOK.com/remote-jobs/remote-senior-python-developer-acme-123"


def test_raw_json_keeps_whole_offer_unchanged(offers: list) -> None:
    assert json.loads(offers[0].raw_json) == SAMPLE[1]
