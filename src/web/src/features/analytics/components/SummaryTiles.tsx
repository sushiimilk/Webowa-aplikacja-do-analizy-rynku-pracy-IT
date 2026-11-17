import { formatMoney, formatNumber, formatPercent } from '@/lib/format';
import type { MarketSummary } from '../types';

type SummaryTilesProps = {
    summary: MarketSummary;
};

export function SummaryTiles({ summary }: SummaryTilesProps) {
    const tiles = [
        { label: 'Liczba ofert', value: formatNumber(summary.offerCount) },
        { label: 'Oferty zdalne', value: formatPercent(summary.remoteShare) },
        {
            label: 'Mediana wynagrodzeń (rocznie)',
            value: summary.medianSalary === null ? '—' : formatMoney(summary.medianSalary, summary.salaryCurrency),
        },
    ];

    return (
        <div className="summary-tiles">
            {tiles.map((tile) => (
                <div key={tile.label} className="summary-tile">
                    <span className="summary-tile__value">{tile.value}</span>
                    <span className="summary-tile__label">{tile.label}</span>
                </div>
            ))}
        </div>
    );
}
