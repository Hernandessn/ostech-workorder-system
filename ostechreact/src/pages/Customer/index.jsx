
import { useEffect, useState } from 'react';

import api from '../../services/api';

import { Container } from '../../components/Container';
import { CreateButton } from '../../components/Buttons/CreateButton';

import { ErrorState } from '../../components/ErrorState';
import { EmptyState } from '../../components/EmptyState';
import { Loading } from '../../components/Loading';
import { CustomerList, CreateCustomer, EditCustomer, DeleteCustomer } from '../../components/CustomerItens';
import { Header } from '../../components/Header';

import { toast } from 'react-toastify';


export const Customer = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState(false);
    const [isEmpty, setIsEmpty] = useState([]);

    const [customerSelected, setCustomerSelected] = useState({
        customerId: '',
        name: '',
        email: '',
        phone: '',
        document: ''
    });

    const [customer, setCustomer] = useState([]);

    const [modalAdd, setModalAdd] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCustomerSelected({
            ...customerSelected,
            [name]: value
        });
        console.log(customerSelected);

    }

    const clearCustomerSelected = () => {
        setCustomerSelected({
            customerId: '',
            name: '',
            email: '',
            phone: '',
            document: ''
        });
    };
    const getCustomer = async () => {
        setIsError(false);
        setIsLoading(true);
        try {
            const response = await api.get('/customer');
            console.log(response.data);
            setCustomer(response.data);
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao carregar a lista!");
        } finally {
            setIsLoading(false);
        }
    }
    const postCustomer = async () => {
        setIsError(false);
        setIsSubmitting(true);
        try {
            const response = await api.post('/customer', {
                name: customerSelected.name,
                email: customerSelected.email,
                phone: customerSelected.phone,
                document: customerSelected.document
            });
            setCustomer(prev => [...prev, response.data]);

            clearCustomerSelected();
            setModalAdd(false);
            toast.success("Cliente criado com sucesso!");
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Error ao criar cliente!");
        } finally {
            setIsSubmitting(false);
        }
    }

    const putCustomer = async () => {
        setIsError(false);
        setIsSubmitting(true);

        try {
            const response = await api.put(
                `/customer/${customerSelected.customerId}`,
                customerSelected
            );

            setCustomer(prev =>
                prev.map(item =>
                    item.customerId === response.data.customerId
                        ? response.data
                        : item
                )
            );

            clearCustomerSelected();
            setModalEdit(false);
            toast.success("Atualizações salvas com sucesso!");
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao atualizar cliente!");
        } finally {
            setIsSubmitting(false);
        }
    };

    const deleteCustomer = async () => {
        setIsError(false);
        setIsSubmitting(true);
        try {
            const response = await api.delete(`/customer/${customerSelected.customerId}`);

            setCustomer(prev =>
                prev.filter(
                    item =>
                        item.customerId !== customerSelected.customerId
                )
            );

            clearCustomerSelected();
            setModalDelete(false);
            toast.success("Cliente deletedo com sucesso!");
        } catch (error) {
            console.log(error)
            setModalDelete(true);
            toast.error("Erro ao deletar cliente!");
        } finally {
            setIsSubmitting(false);
        }
    }

    useEffect(() => {
        getCustomer();
    }, []);

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
                            setModalAdd(true);
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
                                setModalAdd(true);
                            }} />
                    </div>
                    <ul className="flex flex-col gap-3">
                        {customer.map(value => (
                            <CustomerList
                                key={value.customerId}
                                customer={value}
                                onEdit={() => {
                                    setCustomerSelected(value);
                                    setModalEdit(true);
                                }}
                                onDelete={() => {
                                    setCustomerSelected(value);
                                    setModalDelete(true);
                                }} />
                        ))}
                    </ul>
                    <CreateCustomer
                        customer={customerSelected}
                        isOpen={modalAdd}
                        onClose={() => setModalAdd(false)}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={postCustomer}

                    />
                    <EditCustomer
                        customer={customerSelected}
                        isOpen={modalEdit}
                        onClose={() => setModalEdit(false)}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={putCustomer}

                    />
                    <DeleteCustomer
                        customer={customerSelected}
                        isOpen={modalDelete}
                        isSubmitting={isSubmitting}
                        onClose={() => setModalDelete(false)}
                        onConfirm={deleteCustomer}
                    />
                </section>
            )}

        </Container >
    );
}