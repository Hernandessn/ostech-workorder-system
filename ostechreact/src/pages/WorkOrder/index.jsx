import { useEffect, useState } from 'react';

import api from '../../services/api';

import { toast } from 'react-toastify';

import {
    Container,
    Header,
    Loading,
    ErrorState,
    EmptyState
} from '../../components';

import {
    WorkOrderList,
    CreateWorkOrder,
    EditWorkOrder,
    DeleteWorkOrder
} from '../../components/WorkOrderItens';

import {
    useModals,
    useRequestState
} from '../../hooks';

import {
    useMutation,
    useQuery,
    useQueryClient
} from '@tanstack/react-query';

import { validateWorkOrder } from '../../validations/workOrderValidation';

import { getApiErrorMessage } from '../../utils/apiError';
import { workOrderService } from '../../services/workOrderService';



import { CreateButton } from '../../components/Buttons/CreateButton';

export const WorkOrder = () => {
    const {
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

    const [technicians, setTechnicians] = useState([]);
    const [customers, setCustomers] = useState([]);
    const [categories, setCategories] = useState([]);
    const [equipments, setEquipments] = useState([]);


    const [workOrderSelected, setWorkOrderSelected] = useState({
        title: '',
        description: '',
        amount: '',
        deadline: '',
        openingDate: '',
        status: '',
        technicianId: '',
        customerId: '',
        categoryId: '',
        equipmentId: '',
    });

    const clearWorkOrderSelected = () => {
        setWorkOrderSelected({
            title: '',
            description: '',
            amount: '',
            deadline: '',
            openingDate: '',
            status: '',
            technicianId: '',
            customerId: '',
            categoryId: '',
            equipmentId: ''
        });
    }

    const handleChange = (e) => {
        const { name, value } = e.target;
        setWorkOrderSelected({
            ...workOrderSelected,
            [name]: value
        });
    }

    const handleCreateWorkOrder = () => {
        const validationErrors = validateWorkOrder(workOrderSelected);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        setErrors({});

        createWorkOrderMutation.mutate({
            title: workOrderSelected.title,
            description: workOrderSelected.description,
            amount: workOrderSelected.amount,
            deadline: workOrderSelected.deadline,
            openingDate: workOrderSelected.openingDate,
            status: workOrderSelected.status,
            technicianId: workOrderSelected.technicianId,
            customerId: workOrderSelected.customerId,
            categoryId: workOrderSelected.categoryId,
            equipmentId: workOrderSelected.equipmentId,
        });
    };

    const handleUpdateWorkOrder = () => {
        updateWorkOrderMutation.mutate({
            id: workOrderSelected.workOrderId,
            data: workOrderSelected
        });
    };

    const handleDeleteWorkOrder = () => {
        deleteWorkOrderMutation.mutate(
            workOrderSelected.workOrderId
        );
    };

    const {
        data: workOrder = [],
        isLoading,
        isError,
        error
    } = useQuery({
        queryKey: ["workOrders"],
        queryFn: workOrderService.getAll
    });

    const createWorkOrderMutation = useMutation({
        mutationFn: workOrderService.create,
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["workOrders"]
            });

            closeCreate();
            clearWorkOrderSelected();

            toast.success("Ordem de serviço criada com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const updateWorkOrderMutation = useMutation({
        mutationFn: ({ id, data }) =>
            workOrderService.update(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["workOrders"]
            });

            closeEdit();
            clearWorkOrderSelected();

            toast.success("Atualizações salvas com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const deleteWorkOrderMutation = useMutation({
        mutationFn: workOrderService.delete,

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["workOrders"]
            });

            closeDelete();
            clearWorkOrderSelected();

            toast.success("Ordem de serviço deletedo com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });



    useEffect(() => {
        const loadOptions = async () => {
            const [techRes, custRes, catRes, equipRes] = await Promise.all([
                api.get('/technician'),
                api.get('/customer'),
                api.get('/category'),
                api.get('/equipment'),
            ]);
            setTechnicians(techRes.data);
            setCustomers(custRes.data);
            setCategories(catRes.data);
            setEquipments(equipRes.data);
        };
        loadOptions();
    }, []);

    useEffect(() => {
        if (isError) {
            toast.error(getApiErrorMessage(error));
        }
    }, [isError, error]);

    return (
        <Container>
            {isError ? (
                <ErrorState message="Erro ao carregar a lista, tente novamente" />
            ) : isLoading ? (
                <Loading />
            ) : workOrder.length === 0 ? (
                <div className="flex flex-col items-center justify-center gap-4 py-16">
                    <EmptyState message="A lista está vazia, crie um ordem de serviço:" />
                    <CreateButton
                        entity="WorkOrder"
                        onCreate={() => {
                            clearWorkOrderSelected();
                            setErrors({});
                            openCreate();
                        }}
                    />
                </div>

            ) : (
                <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                    <Header />
                    <div className="flex items-center justify-between">
                        <h1 className="text-xl font-semibold text-[#E2E2B6]">WorkOrder List</h1>
                        <CreateButton
                            entity="WorkOrder"
                            onCreate={() => {
                                clearWorkOrderSelected();
                                setErrors({});
                                openCreate();
                            }} />
                    </div>
                    <ul className="flex flex-col gap-3">
                        {workOrder.map(value => (
                            <WorkOrderList
                                key={value.workOrderId}
                                workOrder={value}
                                onEdit={() => {
                                    setWorkOrderSelected(value);
                                    openEdit();
                                }}
                                onDelete={() => {
                                    setWorkOrderSelected(value);
                                    openDelete();
                                }}
                            />
                        ))}
                    </ul>

                    <CreateWorkOrder
                        workOrder={workOrderSelected}
                        technicians={technicians}
                        customers={customers}
                        categories={categories}
                        equipments={equipments}
                        isOpen={isCreateOpen}
                        onClose={closeCreate}
                        isSubmitting={createWorkOrderMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleCreateWorkOrder}
                        errors={errors}
                    />

                    <EditWorkOrder
                        workOrder={workOrderSelected}
                        technicians={technicians}
                        customers={customers}
                        categories={categories}
                        equipments={equipments}
                        isOpen={isEditOpen}
                        onClose={closeEdit}
                        onChange={handleChange}
                        isSubmitting={updateWorkOrderMutation.isPending}
                        onSubmit={handleUpdateWorkOrder}
                        errors={errors}
                    />

                    <DeleteWorkOrder
                        workOrder={workOrderSelected}
                        isOpen={isDeleteOpen}
                        onClose={closeDelete}
                        isSubmitting={deleteWorkOrderMutation.isPending}
                        onConfirm={handleDeleteWorkOrder}
                    />
                </section>

            )}

        </Container>
    );
}