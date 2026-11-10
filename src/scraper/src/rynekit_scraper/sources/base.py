from abc import ABC, abstractmethod
from collections.abc import Iterable

from rynekit_scraper.models import RawOffer


class OfferSource(ABC):
    name: str

    @abstractmethod
    def fetch(self) -> Iterable[RawOffer]:
        raise NotImplementedError
