import { useEffect, useState } from 'react';

import { Container } from '../../components/Container';
import { CreateButton } from '../../components/Buttons/CreateButton';

import { ErrorState } from '../../components/ErrorState';
import { EmptyState } from '../../components/EmptyState';
import { Loading } from '../../components/Loading';
import { CustomerList, CreateCustomer, EditCustomer, DeleteCustomer } from '../../components/CustomerItens';
import { Header } from '../../components/Header';

import { toast } from 'react-toastify';
import { validateCustomer } from '../../validations/customerValidation';
import { getApiErrorMessage } from '../../utils/apiError';
import { useRequestState } from '../../hooks/useRequestState';
import { useModals } from '../../hooks/useModals';
import { customerService } from '../../services/customerService';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';

export const Customer = () => {
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

    const [customerSelected, setCustomerSelected] = useState({
        customerId: '',
        name: '',
        email: '',
        phone: '',
        document: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCustomerSelected({
            ...customerSelected,
            [name]: value
        });
        console.log(customerSelected);
    };

 const handleCreateCustomer = () => {
        const validationErrors = validateCustomer(customerSelected);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        setErrors({});

        createCustomerMutation.mutate({
            name: customerSelected.name,
            email: customerSelected.email,
            phone: customerSelected.phone,
            document: customerSelected.document
        });
    };

    const handleUpdateCustomer = () => {
        updateCustomerMutation.mutate({
            id: customerSelected.customerId,
            data: customerSelected
        });
    };

    const handleDeleteCustomer = () => {
        deleteCustomerMutation.mutate(
            customerSelected.customerId
        );
    };

    const clearCustomerSelected = () => {
        setCustomerSelected({
            customerId: '',
            name: '',
            email: '',
            phone: '',
            document: ''
        });
    };

   
    const {
        data: customer = [],
        isLoading,
        isError,
        error
    } = useQuery({
        queryKey: ["customers"],
        queryFn: customerService.getAll
    });

    const createCustomerMutation = useMutation({
        mutationFn: customerService.create,
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ["customers"]
            });

            closeCreate();
            clearCustomerSelected();

            toast.success("Cliente criado com sucesso!");
        },
        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const updateCustomerMutation = useMutation({
        mutationFn: ({ id, data }) =>
            customerService.update(id, data),

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["customers"]
            });

            closeEdit();
            clearCustomerSelected();

            toast.success("Atualizações salvas com sucesso!");
        },

        onError: (error) => {
            toast.error(getApiErrorMessage(error));
        }
    });

    const deleteCustomerMutation = useMutation({
        mutationFn: customerService.delete,

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ["customers"]
            });

            closeDelete();
            clearCustomerSelected();

            toast.success("Cliente deletedo com sucesso!");
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
                <ErrorState message="Erro ao carregar a lista, tente novamente" />
            ) : isLoading ? (
                <Loading />
            ) : customer.length === 0 ? (
                <div className="flex flex-col items-center justify-center gap-4 py-16">
                    <EmptyState message="A lista está vazia, crie um cliente:" />

                    <CreateButton
                        entity="Customer"
                        onCreate={() => {
                            clearCustomerSelected();
                            setErrors({});
                            openCreate();
                        }}
                    />
                </div>
            ) : (
                <section className="max-w-3xl mx-auto flex flex-col gap-6 px-4 py-6">
                    <Header />
                    <div className="flex items-center justify-between">
                        <h1 className="text-xl font-semibold text-[#E2E2B6]">Customer List</h1>
                        <CreateButton
                            entity="Customer"
                            onCreate={() => {
                                clearCustomerSelected();
                                setErrors({});
                                openCreate();
                            }} />
                    </div>
                    <ul className="flex flex-col gap-3">
                        {customer.map(value => (
                            <CustomerList
                                key={value.customerId}
                                customer={value}
                                onEdit={() => {
                                    setCustomerSelected(value);
                                    openEdit();
                                }}
                                onDelete={() => {
                                    setCustomerSelected(value);
                                    openDelete();
                                }} />
                        ))}
                    </ul>
                    <CreateCustomer
                        customer={customerSelected}
                        isOpen={isCreateOpen}
                        onClose={closeCreate}
                        isSubmitting={createCustomerMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleCreateCustomer}
                        errors={errors}
                    />
                    <EditCustomer
                        customer={customerSelected}
                        isOpen={isEditOpen}
                        onClose={closeEdit}
                        isSubmitting={updateCustomerMutation.isPending}
                        onChange={handleChange}
                        onSubmit={handleUpdateCustomer}
                        errors={errors}
                    />
                    <DeleteCustomer
                        customer={customerSelected}
                        isOpen={isDeleteOpen}
                        isSubmitting={deleteCustomerMutation.isPending}
                        onClose={closeDelete}
                        onConfirm={handleDeleteCustomer}
                    />
                </section>
            )}

        </Container >
    );
}