import { StatusMessage } from '@/components/ui/StatusMessage';
import { useAsync } from '@/lib/use-async';
import { getMarketSummary } from '../api/get-market-summary';
import { getSkillDemand } from '../api/get-skill-demand';
import { SkillDemandChart } from './SkillDemandChart';
import { SummaryTiles } from './SummaryTiles';
import './analytics.css';

function SummarySection() {
    const summary = useAsync(getMarketSummary, []);

    if (summary.status === 'loading') return <StatusMessage>Ładowanie wskaźników...</StatusMessage>;
    if (summary.status === 'error') return <StatusMessage variant="error">Nie udało się pobrać wskaźników.</StatusMessage>;

    return <SummaryTiles summary={summary.data} />;
}

function SkillsSection() {
    const skills = useAsync(getSkillDemand, []);

    if (skills.status === 'loading') return <StatusMessage>Ładowanie technologii...</StatusMessage>;
    if (skills.status === 'error') return <StatusMessage variant="error">Nie udało się pobrać technologii.</StatusMessage>;
    if (skills.data.length === 0) return <StatusMessage>Brak danych o technologiach.</StatusMessage>;

    return <SkillDemandChart skills={skills.data} />;
}

export function AnalyticsPage() {
    return (
        <section>
            <h1>Statystyki rynku</h1>
            <SummarySection />

            <h2>Najpopularniejsze technologie</h2>
            <SkillsSection />
        </section>
    );
}
