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
    assert run([FakeSource(), FakeSource()]) == 4


def test_run_without_sources_returns_zero() -> None:
    assert run([]) == 0
