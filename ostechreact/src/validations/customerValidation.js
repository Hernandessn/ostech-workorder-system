export const validateCustomer = (customer) => {
    const errors = {};

    if (!customer.name?.trim()) {
        errors.name = 'O nome é obrigatório.';
    }

    if (!customer.email?.trim()) {
        errors.email = 'O email é obrigatório.';
    }

    if (!customer.phone) {
        errors.phone = 'O telefone é obrigatório.';
    }

    if (!customer.document) {
        errors.document = 'O documento é obrigatório.';
    }

    return errors;
};