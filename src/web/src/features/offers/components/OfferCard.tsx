import { formatNumber } from '@/lib/format';
import type { OfferListItem } from '../types';
import { workModeLabels } from '../utils/work-mode';

function formatSalary(offer: OfferListItem): string {
    if (offer.salaryMin === null && offer.salaryMax === null) return 'Wynagrodzenie nie podane';

    const min = offer.salaryMin === null ? '?' : formatNumber(offer.salaryMin);
    const max = offer.salaryMax === null ? '?' : formatNumber(offer.salaryMax);

    return `${min} – ${max} ${offer.salaryCurrency ?? ''}`.trim();
}

type OfferCardProps = {
    offer: OfferListItem;
};

export function OfferCard({ offer }: OfferCardProps) {
    const details = [offer.company, offer.location, offer.workMode && workModeLabels[offer.workMode]]
        .filter(Boolean)
        .join(' · ');

    return (
        <article className="offer-card">
            <a className="offer-card__title" href={offer.sourceUrl} target="_blank" rel="noreferrer">
                {offer.title}
            </a>
            <p className="offer-card__details">{details}</p>
            <p className="offer-card__salary">{formatSalary(offer)}</p>
        </article>
    );
}
