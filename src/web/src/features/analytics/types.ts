export type MarketSummary = {
    offerCount: number;
    remoteShare: number;
    medianSalary: number | null;  
    salaryCurrency: string;
};

export type SkillDemand = {
    skill: string;
    offerCount: number;
    share: number;               
};
