import { OffersList } from '@/features/offers/components/OffersList';
import { BackendStatus } from '@/features/system-status/components/BackendStatus';
import './app.css';

export function App() {
    return (
        <main>
            <h1>Analiza rynku pracy IT</h1>
            <BackendStatus />
            <OffersList />
        </main>
    );
}
