import json
import time
from collections.abc import Iterable

import requests

from rynekit_scraper.models import RawOffer
from rynekit_scraper.sources.base import OfferSource

API_URL = "https://solid.jobs/public-api/offers/IT"
PAGE_SIZE = 500
PAUSE_SECONDS = 1


class SolidJobsSource(OfferSource):
    name = "solidjobs"

    def __init__(self, user_agent: str, campaign: str) -> None:
        self.user_agent = user_agent
        self.campaign = campaign

    def fetch(self) -> Iterable[RawOffer]:
        page_index = 0
        while True:
            response = requests.get(
                API_URL,
                params={"campaign": self.campaign, "pageSize": PAGE_SIZE, "pageIndex": page_index},
                headers={"User-Agent": self.user_agent, "X-Api-Version": "1.0"},
                timeout=30,
            )
            response.raise_for_status()
            page = response.json()
            for item in page["jobs"]:
                if not item.get("jobOfferKey") or not item.get("url"):
                    continue
                yield RawOffer(
                    source=self.name,
                    source_offer_id=item["jobOfferKey"],
                    source_url=item["url"],
                    raw_json=json.dumps(item, ensure_ascii=False),
                )
            page_index += 1
            if page_index >= page["totalPages"]:
                break
            time.sleep(PAUSE_SECONDS)
