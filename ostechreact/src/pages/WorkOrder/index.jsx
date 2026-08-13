import { useEffect, useState } from 'react';
import api from '../../services/api';
import logoOstech from '../../assets/logo-ostech.png';
import { Container } from '../../components/Container';
import { toast } from 'react-toastify';
import { ErrorState } from '../../components/ErrorState';
import { Loading } from '../../components/Loading';
import { CreateButton } from '../../components/Buttons/CreateButton';
import { Header } from '../../components/Header';
import { WorkOrderList, CreateWorkOrder, EditWorkOrder, DeleteWorkOrder } from '../../components/WorkOrderItens';
import { EmptyState } from '../../components/EmptyState';
import { validateWorkOrder } from '../../validations/workOrderValidation';
import { useRequestState } from '../../hooks/useRequestState';
import { useModals } from '../../hooks/useModals';
import { getApiErrorMessage } from '../../utils/apiError';

export const WorkOrder = () => {
    const {
        isLoading,
        setIsLoading,
        isSubmitting,
        setIsSubmitting,
        isError,
        setIsError,
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

    const [workOrder, setWorkOrder] = useState([]);

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
        console.log(workOrderSelected);
    }

    const getWorkOrder = async () => {
        setIsError(false);
        setIsLoading(true);
        try {
            const response = await api.get('/workorder');

            console.log(response.data);

            setWorkOrder(response.data);
        } catch (error) {

            setIsError(true);
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsLoading(false);
        }
    }

    const postWorkOrder = async () => {
        try {
            const validationErrors = validateWorkOrder(workOrderSelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }

            setErrors({});
            setIsSubmitting(true);


            const response = await api.post('/workorder', {
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

            setWorkOrder(prev => [...prev, response.data]);

            clearWorkOrderSelected();
            closeCreate();
            toast.success("Ordem de serviço criada com sucesso!");
        } catch (error) {
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false);
        }
    }

    const putWorkOrder = async () => {
        try {
            const validationErrors = validateWorkOrder(workOrderSelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }

            setErrors({});
            setIsSubmitting(true);

            const response = await api.put(`/workorder/${workOrderSelected.workOrderId}`, workOrderSelected);

            setWorkOrder(prev =>
                prev.map(item =>
                    item.workOrderId == workOrderSelected.workOrderId
                        ? response.data
                        : item
                )
            );

            clearWorkOrderSelected();
            closeEdit();
            toast.success("Atualizações salvas com sucesso!");
        } catch (error) {
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false);
        }
    }

    const deleteWorkOrder = async () => {
        setIsSubmitting(true);
        try {
            const response = await api.delete(`/workOrder/${workOrderSelected.workOrderId}`);

            setWorkOrder(prev =>
                prev.filter(item =>
                    item.workOrderId !== workOrderSelected.workOrderId
                )
            );

            clearWorkOrderSelected();
            closeDelete(false);
            toast.success("Ordem de serviço deletedo com sucesso!");
        } catch (error) {
            toast.error(getApiErrorMessage(error));
        } finally {
            setIsSubmitting(false);
        }
    }
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
        getWorkOrder();
    }, [])

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
                        isOpen={openCreate()}
                        onClose={closeCreate()}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={postWorkOrder}
                        errors={errors}
                    />

                    <EditWorkOrder
                        workOrder={workOrderSelected}
                        technicians={technicians}
                        customers={customers}
                        categories={categories}
                        equipments={equipments}
                        isOpen={openEdit()}
                        onClose={closeEdit()}
                        onChange={handleChange}
                        isSubmitting={isSubmitting}
                        onSubmit={putWorkOrder}
                        errors={errors}
                    />
                    <DeleteWorkOrder
                        workOrder={workOrderSelected}
                        isOpen={openDelete()}
                        onClose={closeDelete()}
                        isSubmitting={isSubmitting}
                        onConfirm={deleteWorkOrder}
                    />
                </section>

            )}

        </Container>
    );
}