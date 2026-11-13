import os

import pyodbc

from rynekit_scraper.models import RawOffer

UPSERT_SQL = """
MERGE stg.RawOffers AS t
USING (VALUES (?, ?, ?, ?)) AS s (Source, SourceOfferId, SourceUrl, RawJson)
ON t.Source = s.Source AND t.SourceOfferId = s.SourceOfferId
WHEN MATCHED THEN
    UPDATE SET SourceUrl = s.SourceUrl, RawJson = s.RawJson,
               FetchedAt = SYSUTCDATETIME(), ProcessedAt = NULL
WHEN NOT MATCHED THEN
    INSERT (Source, SourceOfferId, SourceUrl, RawJson)
    VALUES (s.Source, s.SourceOfferId, s.SourceUrl, s.RawJson)
OUTPUT $action;
"""


def connect() -> pyodbc.Connection:
    connection_string = (
        "DRIVER={ODBC Driver 18 for SQL Server};"
        f"SERVER=127.0.0.1,{os.environ.get('MSSQL_PORT', '1433')};"
        "DATABASE=RynekIT;"
        "UID=sa;"
        f"PWD={os.environ['MSSQL_SA_PASSWORD']};"
        "Encrypt=yes;TrustServerCertificate=yes;"
    )
    return pyodbc.connect(connection_string, timeout=10)


class RawOfferWriter:
    def __init__(self, connection: pyodbc.Connection) -> None:
        self.connection = connection
        self.inserted = 0
        self.updated = 0

    def save(self, offer: RawOffer) -> None:
        cursor = self.connection.cursor()
        cursor.execute(
            UPSERT_SQL,
            offer.source,
            offer.source_offer_id,
            offer.source_url,
            offer.raw_json,
        )
        action = cursor.fetchone()[0]
        if action == "INSERT":
            self.inserted += 1
        else:
            self.updated += 1
