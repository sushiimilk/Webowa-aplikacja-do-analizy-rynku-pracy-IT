import { formatNumber, formatPercent } from '@/lib/format';
import type { SkillDemand } from '../types';

type SkillDemandChartProps = {
    skills: SkillDemand[];
};

export function SkillDemandChart({ skills }: SkillDemandChartProps) {
    const max = Math.max(...skills.map((s) => s.share));

    return (
        <ul className="skill-chart">
            {skills.map((s) => (
                <li key={s.skill} className="skill-chart__row">
                    <span className="skill-chart__name">{s.skill}</span>
                    <span className="skill-chart__track">
                        <span className="skill-chart__bar" style={{ width: `${(s.share / max) * 100}%` }} />
                    </span>
                    <span className="skill-chart__value">
                        {formatPercent(s.share)} · {formatNumber(s.offerCount)}
                    </span>
                </li>
            ))}
        </ul>
    );
}
