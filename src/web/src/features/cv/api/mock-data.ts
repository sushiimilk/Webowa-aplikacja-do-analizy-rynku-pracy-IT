import type { CvAnalysis } from '../types';

// Tymczasowy wynik analizy, dopoki backend nie ma endpointu CV.
// Zawsze ten sam, niezaleznie od wgranego pliku.
export const cvAnalysisMock: CvAnalysis = {
    skills: ['javascript', 'typescript', 'react', 'node', 'sql', 'git'],
    matches: [
        {
            offerId: 101,
            title: 'Frontend Developer (React)',
            company: 'Acme Software',
            sourceUrl: 'https://remoteok.com',
            matchPercent: 86,
            matchedSkills: ['javascript', 'typescript', 'react', 'git'],
            missingSkills: ['html'],
        },
        {
            offerId: 102,
            title: 'Full Stack Engineer',
            company: 'DataSoft',
            sourceUrl: 'https://remoteok.com',
            matchPercent: 67,
            matchedSkills: ['javascript', 'node', 'sql', 'react'],
            missingSkills: ['docker', 'postgres'],
        },
        {
            offerId: 103,
            title: 'Backend Developer',
            company: 'CloudNine',
            sourceUrl: 'https://remoteok.com',
            matchPercent: 40,
            matchedSkills: ['node', 'sql'],
            missingSkills: ['docker', 'kubernetes', 'golang'],
        },
    ],
};
