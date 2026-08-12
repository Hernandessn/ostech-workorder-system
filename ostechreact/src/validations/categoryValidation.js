export const validateCategory = (category) => {
    const errors = {};

    if (!category.name?.trim()) {
        errors.name = 'O nome é obrigatório.';
    }

    if (!category.description?.trim()) {
        errors.description = 'A descrição é obrigatória.';
    }
    
    return errors;
};