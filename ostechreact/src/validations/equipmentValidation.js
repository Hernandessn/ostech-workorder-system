export const validateEquipment = (equipment) => {
    const errors = {};

    if (!equipment.name?.trim()) {
        errors.name = 'O nome é obrigatório.';
    }

    if (!equipment.brand?.trim()) {
        errors.brand = 'A marca é obrigatória.';
    }

    if (!equipment.model) {
        errors.model = 'O modelo é obrigatório.';
    }

    if (!equipment.serialNumber) {
        errors.serialNumber = 'O número de série é obrigatório.';
    }

    return errors;
};