import json
from collections.abc import Iterable

import requests

from rynekit_scraper.models import RawOffer
from rynekit_scraper.sources.base import OfferSource

API_URL = "https://remoteok.com/api"


class RemoteOkSource(OfferSource):
    name = "remoteok"

    def __init__(self, user_agent: str) -> None:
        self.user_agent = user_agent

    def fetch(self) -> Iterable[RawOffer]:
        response = requests.get(API_URL, headers={"User-Agent": self.user_agent}, timeout=30)
        response.raise_for_status()
        items = response.json()
        for item in items:
            if "id" not in item or not item.get("url"):
                continue
            yield RawOffer(
                source=self.name,
                source_offer_id=str(item["id"]),
                source_url=item["url"],
                raw_json=json.dumps(item, ensure_ascii=False),
            )