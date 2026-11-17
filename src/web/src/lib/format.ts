const numberFormat = new Intl.NumberFormat('pl-PL');

export function formatNumber(value: number): string {
    return numberFormat.format(value);
}

export function formatPercent(share: number): string {
    return `${Math.round(share * 100)}%`;
}

export function formatMoney(value: number, currency: string): string {
    return new Intl.NumberFormat('pl-PL', { style: 'currency', currency, maximumFractionDigits: 0 }).format(value);
}
