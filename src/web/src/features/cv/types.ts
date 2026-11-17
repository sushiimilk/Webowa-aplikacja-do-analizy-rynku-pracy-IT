export type CvOfferMatch = {
    offerId: number;
    title: string;
    company: string;
    sourceUrl: string;
    matchPercent: number;     // 0-100
    matchedSkills: string[];  // umiejetnosci z CV, ktorych oferta wymaga
    missingSkills: string[];  // wymagane przez oferte, a brakujace w CV
};

export type CvAnalysis = {
    skills: string[];         // umiejetnosci rozpoznane w CV
    matches: CvOfferMatch[];  // najlepiej dopasowane oferty, od najlepszej
};
