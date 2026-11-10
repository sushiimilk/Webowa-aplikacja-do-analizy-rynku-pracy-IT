from rynekit_scraper.sources import OfferSource


def run(sources: list[OfferSource]) -> int:
    count = 0
    for source in sources:
        for _offer in source.fetch():
            count += 1
    return count


def main() -> None:
    print(f"Pobrano ofert: {run([])}")


if __name__ == "__main__":
    main()
