import api from '../../services/api';
import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Container } from '../../components/Container';
import { ErrorState } from '../../components/ErrorState';
import { Loading } from '../../components/Loading';
import { EmptyState } from '../../components/EmptyState';
import { CreateButton } from '../../components/Buttons/CreateButton';
import { Header } from '../../components/Header';

import { EquipmentList, CreateEquipment, EditEquipment, DeleteEquipment } from '../../components/EquipmentItens';
import { validateEquipment } from '../../validations/equipmentValidation';
import { getApiErrorMessage } from '../../utils/apiError.js';
import { useRequestState } from '../../hooks/useRequestState.js';
import { useModals } from '../../hooks/useModals.js';
import { equipmentService } from '../../services/equipmentService.js';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';

export const Equipment = () => {
    const {
        setIsSubmitting,
        errors,
        setErrors
    } = useRequestState();

    const {
        isCreateOpen,
        isEditOpen,
        isDeleteOpen,
        openCreate,
        closeCreate,
        openEdit,
        closeEdit,
        openDelete,
        closeDelete
    } = useModals();

    const queryClient = useQueryClient();

    const [equipmentSelected, setEquipmentSelected] = useState({
        equipmentId: '',
        name: '',
        brand: '',
        model: '',
        serialNumber: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEquipmentSelected({
            ...equipmentSelected,
            [name]: value
        });
        console.log(equipmentSelected);
    };

    const clearEquipmentSelected = () => {
        setEquipmentSelected({
            equipmentId: '',
            name: '',
            brand: '',
            model: '',
            serialNumber: ''
        });
    };


    const handleCreateEquipment = () => {
        const validationErrors = validateEquipment(equipmentSelected);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        setErrors({});

        createEquipmentMutation.mutate({
            name: equipmentSelected.name,
            brand: equipmentSelected.brand,
            model: equipmentSelected.model,
            serialNumber: equipmentSelected.serialNumber
        });
    };

    const handleUpdateEquipment = () => {
        updateEquipmentMutation.mutate({
            id: equipmentSelected.equipmentId,
            data: equipmentSelected
        });
    };

    const handleDeleteEquipment = () => {
        deleteEquipmentMutation.mutate(
            equipmentSelected.equipmentId
        );
    };

    const {
        data: equipment = [],
        isLoading,
        isError,
        error
    } = useQuery({
        queryKey: ["equipments"],
        queryFn: equipmentService.getAll
    });

    const createEquipmentMutation = useMutation({
        mutationFn: equipmentService.create,
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["equipments"]
            });

            closeCreate();
            clearEquipmentSelected();

            toast.success("Equipamento criado com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const updateEquipmentMutation = useMutation({
        mutationFn: ({ id, data }) =>
            equipmentService.update(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["equipments"]
            });

            closeEdit();
            clearEquipmentSelected();

            toast.success("Atualizações salvas com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const deleteEquipmentMutation = useMutation({
        mutationFn: equipmentService.delete,

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["equipments"]
            });

            closeDelete();
            clearEquipmentSelected();

            toast.success("Equipamento deletado com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    useEffect(() => {
        if (isError) {
            toast.error(getApiErrorMessage(error));
        }
    }, [isError, error]);

    return (
        <Container>

            {isError ? (
                <ErrorState message="Erro ao carregar a lista, tente novamente!" />
            ) : isLoading ? (
                <Loading />
            ) : equipment.length === 0 ? (
                <div className="flex flex-col items-center justify-center gap-4 py-16">
                    <EmptyState message="A lista está vazia, crie um equipamento:" />
                    <CreateButton
                        entity="Equipment"
                        onCreate={() => {
                            clearEquipmentSelected()
                            setErrors({});
                            openCreate();
                        }}
                    />
                </div>
            ) : (
                <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                    <Header />
                    <div className="flex items-center justify-between">
                        <h1 className="text-xl font-semibold text-[#E2E2B6]">Equipment List</h1>
                        <CreateButton
                            entity="Equipment"
                            onCreate={() => {
                                clearEquipmentSelected()
                                setErrors({});
                                openCreate();
                            }}
                        />
                    </div>
                    <ul className="flex flex-col gap-3">
                        {equipment.map(value => (
                            <EquipmentList
                                key={value.equipmentId}
                                equipment={value}
                                onEdit={() => {
                                    setEquipmentSelected(value);
                                    openEdit();
                                }}
                                onDelete={() => {
                                    setEquipmentSelected(value);
                                    openDelete();
                                }}
                            />
                        ))}
                    </ul>
                    <CreateEquipment
                        equipment={equipmentSelected}
                        isOpen={isCreateOpen}
                        onClose={closeCreate}
                        isSubmitting={createEquipmentMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleCreateEquipment}
                        errors={errors}
                    />
                    <EditEquipment
                        equipment={equipmentSelected}
                        isOpen={isEditOpen}
                        onClose={closeEdit}
                        isSubmitting={updateEquipmentMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleUpdateEquipment}
                        errors={errors}
                    />
                    <DeleteEquipment
                        equipment={equipmentSelected}
                        isOpen={isDeleteOpen}
                        onClose={closeDelete}
                        isSubmitting={deleteEquipmentMutation.isPending}
                        onConfirm={handleDeleteEquipment}
                    />
                </section>
            )}
        </Container>
    )
}