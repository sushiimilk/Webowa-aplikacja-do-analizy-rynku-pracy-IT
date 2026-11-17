import { StatusMessage } from '@/components/ui/StatusMessage';
import type { CvAnalysis, CvOfferMatch } from '../types';
import { SkillTag } from './SkillTag';

function MatchCard({ match }: { match: CvOfferMatch }) {
    return (
        <article className="cv-match">
            <div className="cv-match__header">
                <a className="cv-match__title" href={match.sourceUrl} target="_blank" rel="noreferrer">
                    {match.title}
                </a>
                <span className="cv-match__percent">{match.matchPercent}%</span>
            </div>
            <p className="cv-match__company">{match.company}</p>

            <div className="cv-match__track">
                <span className="cv-match__bar" style={{ width: `${match.matchPercent}%` }} />
            </div>

            <div className="skill-tags">
                {match.matchedSkills.map((skill) => (
                    <SkillTag key={skill} name={skill} variant="matched" />
                ))}
                {match.missingSkills.map((skill) => (
                    <SkillTag key={skill} name={skill} variant="missing" />
                ))}
            </div>
        </article>
    );
}

type CvAnalysisResultProps = {
    analysis: CvAnalysis;
};

export function CvAnalysisResult({ analysis }: CvAnalysisResultProps) {
    return (
        <div className="cv-result">
            <h2>Rozpoznane umiejętności</h2>
            {analysis.skills.length === 0 ? (
                <StatusMessage>Nie rozpoznano żadnych umiejętności w CV.</StatusMessage>
            ) : (
                <div className="skill-tags">
                    {analysis.skills.map((skill) => (
                        <SkillTag key={skill} name={skill} />
                    ))}
                </div>
            )}

            <h2>Najlepiej dopasowane oferty</h2>
            <p className="cv-result__legend">
                <SkillTag name="masz" variant="matched" /> <SkillTag name="brakuje" variant="missing" />
            </p>
            {analysis.matches.length === 0 ? (
                <StatusMessage>Brak ofert pasujących do CV.</StatusMessage>
            ) : (
                <div className="cv-matches">
                    {analysis.matches.map((match) => (
                        <MatchCard key={match.offerId} match={match} />
                    ))}
                </div>
            )}
        </div>
    );
}
