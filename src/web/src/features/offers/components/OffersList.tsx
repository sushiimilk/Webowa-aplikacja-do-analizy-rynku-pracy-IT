import { useAsync } from '@/lib/use-async';
import { getOffers } from '../api/get-offers';
import type { OfferListItem } from '../types';

function formatSalary(offer: OfferListItem): string {
    if (offer.salaryMin === null && offer.salaryMax === null) return '-';
    return `${offer.salaryMin ?? '?'} - ${offer.salaryMax ?? '?'} ${offer.salaryCurrency ?? ''}`;
}

export function OffersList() {
    const offers = useAsync(() => getOffers({ pageSize: 20 }), []);

    if (offers.status === 'loading') return <p>Ładowanie ofert...</p>;
    if (offers.status === 'error') return <p>Nie udało się pobrać ofert.</p>;
    if (offers.data.totalCount === 0) return <p>Brak ofert w bazie.</p>;

    return (
        <table>
            <thead>
                <tr>
                    <th>Stanowisko</th>
                    <th>Firma</th>
                    <th>Lokalizacja</th>
                    <th>Tryb</th>
                    <th>Wynagrodzenie</th>
                </tr>
            </thead>
            <tbody>
                {offers.data.items.map((offer) => (
                    <tr key={offer.id}>
                        <td><a href={offer.sourceUrl} target="_blank" rel="noreferrer">{offer.title}</a></td>
                        <td>{offer.company}</td>
                        <td>{offer.location ?? '-'}</td>
                        <td>{offer.workMode ?? '-'}</td>
                        <td>{formatSalary(offer)}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}
