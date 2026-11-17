import { useState, type FormEvent } from 'react';
import type { OfferFilters, WorkMode } from '../types';
import { parseWorkMode, workModeLabels, workModes } from '../utils/work-mode';

type OfferFiltersFormProps = {
    initial: OfferFilters;
    onSubmit: (filters: OfferFilters) => void;
};

// Formularz trzyma wpisywane wartosci u siebie i oddaje je dopiero po kliknieciu „Szukaj”,
// zeby nie wysylac zapytania do backendu przy kazdej wpisanej literze.
export function OfferFiltersForm({ initial, onSubmit }: OfferFiltersFormProps) {
    const [phrase, setPhrase] = useState(initial.phrase);
    const [workMode, setWorkMode] = useState<WorkMode | null>(initial.workMode);

    function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();
        onSubmit({ phrase: phrase.trim(), workMode });
    }

    function handleClear() {
        setPhrase('');
        setWorkMode(null);
        onSubmit({ phrase: '', workMode: null });
    }

    return (
        <form className="offer-filters" onSubmit={handleSubmit} role="search">
            <label className="offer-filters__field offer-filters__field--grow">
                <span>Szukaj</span>
                <input
                    type="search"
                    value={phrase}
                    onChange={(e) => setPhrase(e.target.value)}
                    placeholder="Stanowisko lub firma, np. React"
                />
            </label>

            <label className="offer-filters__field">
                <span>Tryb pracy</span>
                <select value={workMode ?? ''} onChange={(e) => setWorkMode(parseWorkMode(e.target.value))}>
                    <option value="">Dowolny</option>
                    {workModes.map((mode) => (
                        <option key={mode} value={mode}>
                            {workModeLabels[mode]}
                        </option>
                    ))}
                </select>
            </label>

            <div className="offer-filters__actions">
                <button type="submit" className="button button--primary">
                    Szukaj
                </button>
                <button type="button" className="button" onClick={handleClear}>
                    Wyczyść
                </button>
            </div>
        </form>
    );
}
