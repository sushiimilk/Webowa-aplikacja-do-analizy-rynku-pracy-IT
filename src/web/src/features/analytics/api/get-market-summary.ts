import type { MarketSummary } from '../types';
import { fakeRequest } from '@/lib/fake-request';
import { marketSummaryMock } from './mock-data';

// TODO: gdy backend bedzie gotowy -> return apiGet<MarketSummary>('analytics/summary');
export function getMarketSummary(): Promise<MarketSummary> {
    return fakeRequest(marketSummaryMock);
}
