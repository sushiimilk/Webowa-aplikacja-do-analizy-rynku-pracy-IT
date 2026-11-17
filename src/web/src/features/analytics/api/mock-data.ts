import type { MarketSummary, SkillDemand } from '../types';

export const marketSummaryMock: MarketSummary = {
    offerCount: 1243,
    remoteShare: 0.62,
    medianSalary: 95000,
    salaryCurrency: 'USD',
};

export const skillDemandMock: SkillDemand[] = [
    { skill: 'javascript', offerCount: 412, share: 0.33 },
    { skill: 'python', offerCount: 365, share: 0.29 },
    { skill: 'react', offerCount: 298, share: 0.24 },
    { skill: 'sql', offerCount: 251, share: 0.2 },
    { skill: 'docker', offerCount: 187, share: 0.15 },
    { skill: 'kubernetes', offerCount: 120, share: 0.1 },
];
