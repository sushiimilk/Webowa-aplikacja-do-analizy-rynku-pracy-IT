import { useSearchParams } from 'react-router';
import type { OfferFilters } from '../types';
import { parseWorkMode } from '../utils/work-mode';
import { OfferFiltersForm } from './OfferFiltersForm';
import { OffersList } from './OffersList';

// Filtry i numer strony trzymamy w adresie (?phrase=react&workMode=Remote&page=2),
// dzieki temu link do wynikow mozna skopiowac, a przycisk „Wstecz” w przegladarce dziala.
export function OffersPage() {
    const [searchParams, setSearchParams] = useSearchParams();

    const filters: OfferFilters = {
        phrase: searchParams.get('phrase') ?? '',
        workMode: parseWorkMode(searchParams.get('workMode')),
    };
    const page = Math.max(1, Number(searchParams.get('page')) || 1);

    function updateUrl(next: OfferFilters, nextPage: number) {
        const params = new URLSearchParams();
        if (next.phrase) params.set('phrase', next.phrase);
        if (next.workMode) params.set('workMode', next.workMode);
        if (nextPage > 1) params.set('page', String(nextPage));
        setSearchParams(params);
    }

    return (
        <section>
            <h1>Oferty pracy</h1>

            {/* key: gdy filtry w adresie zmienia sie z zewnatrz (np. „Wstecz”), formularz startuje od nowa */}
            <OfferFiltersForm
                key={`${filters.phrase}|${filters.workMode}`}
                initial={filters}
                onSubmit={(next) => updateUrl(next, 1)}
            />

            <OffersList filters={filters} page={page} onPageChange={(nextPage) => updateUrl(filters, nextPage)} />
        </section>
    );
}
