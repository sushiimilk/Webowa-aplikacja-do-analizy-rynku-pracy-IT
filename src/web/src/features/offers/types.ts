export type WorkMode = 'Remote' | 'Hybrid' | 'Onsite';

export type OfferListItem = {
    id: number;
    title: string;
    company: string;
    location: string | null;
    workMode: WorkMode | null;
    salaryMin: number | null;
    salaryMax: number | null;
    salaryCurrency: string | null;
    sourceUrl: string;
    publishedAt: string | null;
};

export type PagedResult<T> = {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
};
