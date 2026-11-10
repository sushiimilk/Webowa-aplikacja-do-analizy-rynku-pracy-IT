import { apiGet } from '@/lib/api-client';
import type { OfferListItem, PagedResult, WorkMode } from '../types';

export type GetOffersParams = {
    phrase?: string;
    workMode?: WorkMode;
    page?: number;
    pageSize?: number;
};

export function getOffers(params: GetOffersParams = {}): Promise<PagedResult<OfferListItem>> {
    return apiGet<PagedResult<OfferListItem>>('offers', params);
}
