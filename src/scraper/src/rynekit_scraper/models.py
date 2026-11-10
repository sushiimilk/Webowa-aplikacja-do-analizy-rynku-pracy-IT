from dataclasses import dataclass


@dataclass(frozen=True)
class RawOffer:
    source: str
    source_offer_id: str
    source_url: str
    raw_json: str
