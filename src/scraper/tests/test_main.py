from collections.abc import Iterable

from rynekit_scraper.main import run
from rynekit_scraper.models import RawOffer
from rynekit_scraper.sources import OfferSource


class FakeSource(OfferSource):
    name = "fake"

    def fetch(self) -> Iterable[RawOffer]:
        yield RawOffer("fake", "1", "https://example.com/1", "{}")
        yield RawOffer("fake", "2", "https://example.com/2", "{}")


def test_run_counts_offers_from_all_sources() -> None:
    assert run([FakeSource(), FakeSource()], save=lambda offer: None) == 4


def test_run_passes_every_offer_to_save() -> None:
    saved: list[RawOffer] = []
    run([FakeSource()], save=saved.append)
    assert [offer.source_offer_id for offer in saved] == ["1", "2"]


def test_run_without_sources_returns_zero() -> None:
    assert run([], save=lambda offer: None) == 0

class BrokenSource(OfferSource):
    name = "broken"

    def fetch(self) -> Iterable[RawOffer]:
        raise RuntimeError("Source not responding")

def test_failing_source_does_not_stop_run() -> None:
    assert run([BrokenSource(), FakeSource()], save=lambda offer: None) == 2