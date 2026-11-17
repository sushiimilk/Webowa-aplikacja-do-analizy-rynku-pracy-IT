import { StatusMessage } from '@/components/ui/StatusMessage';
import { formatNumber } from '@/lib/format';
import { useAsync } from '@/lib/use-async';
import { getOffers } from '../api/get-offers';
import type { OfferFilters } from '../types';
import { OfferCard } from './OfferCard';
import { OffersPagination } from './OffersPagination';
import './offers.css';

const PAGE_SIZE = 20;

type OffersListProps = {
    filters: OfferFilters;
    page: number;
    onPageChange: (page: number) => void;
};

export function OffersList({ filters, page, onPageChange }: OffersListProps) {
    const { phrase, workMode } = filters;
    const offers = useAsync(
        () => getOffers({ phrase, workMode: workMode ?? undefined, page, pageSize: PAGE_SIZE }),
        [phrase, workMode, page],
    );

    if (offers.status === 'loading') return <StatusMessage>Ładowanie ofert...</StatusMessage>;
    if (offers.status === 'error') return <StatusMessage variant="error">Nie udało się pobrać ofert.</StatusMessage>;

    const { items, totalCount, totalPages } = offers.data;

    if (totalCount === 0) {
        const hasFilters = phrase !== '' || workMode !== null;
        return <StatusMessage>{hasFilters ? 'Brak ofert spełniających kryteria.' : 'Brak ofert w bazie.'}</StatusMessage>;
    }

    return (
        <>
            <p className="offers-count">Znaleziono ofert: {formatNumber(totalCount)}</p>

            <div className="offers-grid">
                {items.map((offer) => (
                    <OfferCard key={offer.id} offer={offer} />
                ))}
            </div>

            <OffersPagination page={page} totalPages={totalPages} onPageChange={onPageChange} />
        </>
    );
}
