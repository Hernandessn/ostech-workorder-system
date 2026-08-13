import { useState } from "react";

export const useModals = () => {
    const [isCreateOpen, setIsCreateOpen] = useState(false);
    const [isEditOpen, setIsEditOpen] = useState(false);
    const [isDeleteOpen, setIsDeleteOpen] = useState(false);

    return {
        isCreateOpen,
        isEditOpen,
        isDeleteOpen,
        openCreate: () => setIsCreateOpen(true),
        closeCreate: () => setIsCreateOpen(false),
        openEdit: () => setIsEditOpen(true),
        closeEdit: () => setIsEditOpen(false),
        openDelete: () => setIsDeleteOpen(true),
        closeDelete: () => setIsDeleteOpen(false),
    };
};