export const validateTechnician = (technician) => {
    const errors = {};

    if (!technician.name?.trim()) {
        errors.name = 'O nome é obrigatório.';
    }

    if (!technician.specialty?.trim()) {
        errors.specialty = 'A especialidade é obrigatória.';
    }

    if (!technician.contact) {
        errors.contact = 'O contato é obrigatório.';
    }

    if (technician.availability === undefined || technician.availability === null) {
        errors.availability = 'A disponibilidade é obrigatória.';
    }

    if (!technician.hiringDate) {
        errors.hiringDate = 'A data de contratação é obrigatória.';
    }

    return errors;
};