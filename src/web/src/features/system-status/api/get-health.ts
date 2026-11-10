import { apiGet } from '@/lib/api-client';

export type HealthResponse = {
    status: string;
    time: string;
};

export function getHealth(): Promise<HealthResponse> {
    return apiGet<HealthResponse>('health');
}
