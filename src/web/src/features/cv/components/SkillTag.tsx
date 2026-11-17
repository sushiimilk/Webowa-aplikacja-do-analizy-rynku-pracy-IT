type SkillTagProps = {
    name: string;
    variant?: 'neutral' | 'matched' | 'missing';
};

export function SkillTag({ name, variant = 'neutral' }: SkillTagProps) {
    return <span className={`skill-tag skill-tag--${variant}`}>{name}</span>;
}
