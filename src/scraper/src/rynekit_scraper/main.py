import logging
import os
from collections.abc import Callable

from dotenv import find_dotenv, load_dotenv

from rynekit_scraper.db import RawOfferWriter, connect
from rynekit_scraper.models import RawOffer
from rynekit_scraper.sources import OfferSource
from rynekit_scraper.sources.remoteok import RemoteOkSource

logger = logging.getLogger(__name__)

DEFAULT_USER_AGENT = "RynekIT-scraper/0.1 (projekt inzynierski)"

def run(sources: list[OfferSource], save: Callable[[RawOffer], None]) -> int:
    count = 0
    for source in sources:
        try:
            for offer in source.fetch():
                save(offer)
                count += 1
        except Exception:
            logger.exception("Źródło %s nie zadziałało", source.name)
    return count


def main() -> None:
    logging.basicConfig(level=logging.INFO)
    load_dotenv(find_dotenv(usecwd=True))

    user_agent = os.environ.get("SCRAPER_USER_AGENT", DEFAULT_USER_AGENT)

    sources = [RemoteOkSource(user_agent)]

    with connect() as connection:
        writer = RawOfferWriter(connection)
        total = run(sources, save=writer.save)
        connection.commit()

    print(f"Pobrano ofert: {total} (nowe: {writer.inserted}, zaktualizowane: {writer.updated})")


if __name__ == "__main__":
    main()
