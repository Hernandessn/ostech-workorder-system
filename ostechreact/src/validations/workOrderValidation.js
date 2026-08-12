export const validateWorkOrder = (workOrder) => {
    const errors = {};

    if (!workOrder.title?.trim()) {
        errors.title = 'O título é obrigatório.';
    }

    if (!workOrder.description?.trim()) {
        errors.description = 'A descrição é obrigatória.';
    }

    if (!workOrder.customerId) {
        errors.customerId = 'O cliente é obrigatório.';
    }

    if (!workOrder.technicianId) {
        errors.technicianId = 'O técnico é obrigatório.';
    }

    if (!workOrder.categoryId) {
        errors.categoryId = 'A categoria é obrigatória.';
    }

    if (!workOrder.equipmentId) {
        errors.equipmentId = 'O equipamento é obrigatório.';
    }

    if (!workOrder.amount || workOrder.amount <= 0) {
        errors.amount = 'O valor deve ser maior que zero.';
    }

    if (!workOrder.openingDate) {
        errors.openingDate = 'A data de abertura é obrigatória.';
    }

    if (!workOrder.deadline) {
        errors.deadline = 'O prazo é obrigatório.';
    }

    if (
        workOrder.openingDate &&
        workOrder.deadline &&
        new Date(workOrder.deadline) < new Date(workOrder.openingDate)
    ) {
        errors.deadline = 'O prazo não pode ser anterior à abertura.';
    }

    return errors;
};