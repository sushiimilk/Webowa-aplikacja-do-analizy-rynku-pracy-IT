import { useAsync } from '@/lib/use-async';
import { getHealth } from '../api/get-health';

export function BackendStatus() {
    const health = useAsync(getHealth, []);

    if (health.status === 'loading') return <p>Status backendu: łączenie...</p>;
    if (health.status === 'error') return <p>Status backendu: brak połączenia z API</p>;

    return <p>Status backendu: {health.data.status}</p>;
}
