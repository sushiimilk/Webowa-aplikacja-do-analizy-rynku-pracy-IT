type OffersPaginationProps = {
    page: number;
    totalPages: number;
    onPageChange: (page: number) => void;
};

export function OffersPagination({ page, totalPages, onPageChange }: OffersPaginationProps) {
    if (totalPages <= 1) return null;

    return (
        <nav className="pagination" aria-label="Strony wyników">
            <button type="button" className="button" disabled={page <= 1} onClick={() => onPageChange(page - 1)}>
                ← Poprzednia
            </button>
            <span>
                Strona {page} z {totalPages}
            </span>
            <button
                type="button"
                className="button"
                disabled={page >= totalPages}
                onClick={() => onPageChange(page + 1)}
            >
                Następna →
            </button>
        </nav>
    );
}
