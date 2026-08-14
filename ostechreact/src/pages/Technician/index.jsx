import { useEffect, useState } from 'react';
import api from '../../services/api';

import { toast } from 'react-toastify';
import { Container } from '../../components/Container';
import { ErrorState } from '../../components/ErrorState';
import { Loading } from '../../components/Loading';
import { EmptyState } from '../../components/EmptyState';
import { CreateButton } from '../../components/Buttons/CreateButton';
import { Header } from '../../components/Header';
import { TechnicianList, CreateTechnician, DeleteTechnician, EditTechnician } from '../../components/TechnicianItens';
import { validateTechnician } from '../../validations/technicianValidation';
import { getApiErrorMessage } from '../../utils/apiError.js';
import { useRequestState } from '../../hooks/useRequestState.js';
import { useModals } from '../../hooks/useModals.js';
import { technicianService } from '../../services/technicianService.js';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';


export const Technician = () => {
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

    const [technicianSelected, setTechnicianSelected] = useState({
        technicianId: '',
        name: '',
        specialty: '',
        contact: '',
        availability: false,
        hiringDate: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setTechnicianSelected({
            ...technicianSelected,
            [name]: name === 'availability' ? value === 'true' : value
        });
    };

    
        const handleCreateTechnician = () => {
            const validationErrors = validateTechnician(technicianSelected);
    
            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }
    
            setErrors({});
    
            createTechnicianMutation.mutate({
                name: technicianSelected.name,
                specialty: technicianSelected.specialty,
                contact: technicianSelected.contact,
                availability: technicianSelected.availability,
                hiringDate: technicianSelected.hiringDate
            });
        };
    
        const handleUpdateTechnician = () => {
            updateTechnicianMutation.mutate({
                id: technicianSelected.technicianId,
                data: technicianSelected
            });
        };
    
        const handleDeleteTechnician = () => {
            deleteTechnicianMutation.mutate(
                technicianSelected.technicianId
            );
        };
    

    const clearTechnicianSelected = () => {
        setTechnicianSelected({
            technicianId: '',
            name: '',
            specialty: '',
            contact: '',
            availability: false,
            hiringDate: ''
        });
    };

    const {
        data: technician = [],
        isLoading,
        isError,
        error
    } = useQuery({
        queryKey: ["technicians"],
        queryFn: technicianService.getAll
    });

    const createTechnicianMutation = useMutation({
        mutationFn: technicianService.create,
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["technicians"]
            });

            closeCreate();
            clearTechnicianSelected();

            toast.success("Técnico criado com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const updateTechnicianMutation = useMutation({
        mutationFn: ({ id, data }) =>
            technicianService.update(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["technicians"]
            });

            closeEdit();
            clearTechnicianSelected();

            toast.success("Atualizações salvas com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const deleteTechnicianMutation = useMutation({
        mutationFn: technicianService.delete,

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["technicians"]
            });

            closeDelete();
            clearTechnicianSelected();

            toast.success("Técnico deletedo com sucesso!");
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
            ) : technician.length === 0 ? (
                <div className="flex flex-col items-center justify-center gap-4 py-16">
                    <EmptyState message="A lista tá vazia, crie um técnico:" />
                    <CreateButton
                        entity="Técnico"
                        onCreate={() => {
                            clearTechnicianSelected();
                            setErrors({});
                            openCreate();
                        }}
                    />
                </div>
            ) : (
                <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                    <Header />
                    <div className="flex items-center justify-between">
                        <h1 className="text-xl font-semibold text-[#E2E2B6]">Technician List</h1>
                        <CreateButton
                            entity="Técnico"
                            onCreate={() => {
                                clearTechnicianSelected();
                                setErrors({});
                                openCreate();
                            }}
                        />
                    </div>
                    <ul className="flex flex-col gap-4">
                        {technician.map(value => (
                            <TechnicianList
                                key={value.technicianId}
                                technician={value}
                                onEdit={() => {
                                    setTechnicianSelected(value);
                                    openEdit();
                                }}
                                onDelete={() => {
                                    setTechnicianSelected(value);
                                    openDelete();
                                }}
                            />
                        ))}
                    </ul>
                    <CreateTechnician
                        technician={technicianSelected}
                        isOpen={isCreateOpen}
                        onClose={closeCreate}
                        isSubmitting={createTechnicianMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleCreateTechnician}
                        errors={errors}
                    />
                    <EditTechnician
                        technician={technicianSelected}
                        isOpen={isEditOpen}
                        isSubmitting={updateTechnicianMutation.isPending}
                        onChange={handleChange}
                        onClose={closeEdit}
                        onSubmit={handleUpdateTechnician}
                        errors={errors}
                    />
                    <DeleteTechnician
                        technician={technicianSelected}
                        isOpen={isDeleteOpen}
                        onClose={closeDelete}
                        isSubmitting={deleteTechnicianMutation.isPending}
                        onConfirm={handleDeleteTechnician}
                    />
                </section>
            )}

        </Container>

    );
}