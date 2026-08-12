import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';
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

export const Technician = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState(false);
    const [errors, setErrors] = useState({});
    const [isEmpty, setIsEmpty] = useState([]);

    const [modalAdd, setModalAdd] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);

    const [technician, setTechnician] = useState([]);
    const [technicianSelected, setTechnicianSelected] = useState({
        technicianId: '',
        name: '',
        specialty: '',
        contact: '',
        availability: '',
        hiringDate: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;

        setTechnicianSelected({
            ...technicianSelected,
            [name]: name === 'availability'
                ? value === 'true'
                : value
        });
    };

    const clearTechnicianSelected = () => {
        setTechnicianSelected({
            technicianId: '',
            name: '',
            specialty: '',
            contact: '',
            availability: '',
            hiringDate: ''
        });
    };

    const getTechnician = async () => {
        setIsError(false);
        setIsLoading(true);
        try {
            const response = await api.get('/technician');

            setTechnician(response.data);
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao carregar a lista!");
        } finally {
            setIsLoading(false);
        }
    }

    const postTechnician = async () => {
        try {
            const validationErrors = validateTechnician(technicianSelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }
            setErrors({});
            setIsSubmitting(true);

            const response = await api.post('/technician', {
                name: technicianSelected.name,
                specialty: technicianSelected.specialty,
                contact: technicianSelected.contact,
                availability: technicianSelected.availability,
                hiringDate: technicianSelected.hiringDate
            });

            setTechnician(prev => [...prev, response.data]);

            clearTechnicianSelected();
            setModalAdd(false);
            toast.success("Técnico criado com sucesso!");
        } catch (error) {
            console.log('STATUS:', error.response?.status);
            console.log('DATA:', error.response?.data);
            console.log('REQUEST:', error.config?.data);
            toast.error("Erro ao criar técnico!");
        } finally {
            setIsSubmitting(false);
        }
    }

    const putTechnician = async () => {
        try {
            const validationErrors = validateTechnician(technicianSelected);

            if (Object.keys(validationErrors).length > 0) {
                setErrors(validationErrors);
                return;
            }
            setErrors({});
            setIsSubmitting(true);

            const response = await api.put(`/technician/${technicianSelected.technicianId}`, technicianSelected);

            setTechnician(prev =>
                prev.map(item =>
                    item.technicianId === technicianSelected.technicianId
                        ? response.data
                        : item
                )
            );

            clearTechnicianSelected();
            setModalEdit(false);
            toast.success("Atualizações salvas com sucesso!");
        } catch (error) {
            console.log(error);
            toast.error("Erro ao atualizar o técnico!");
        } finally {
            setIsSubmitting(false);
        }
    }

    const deleteTechnician = async () => {
        setIsSubmitting(true);
        try {
            const response = await api.delete(`/technician/${technicianSelected.technicianId}`);

            setTechnician(prev =>
                prev.filter(item =>
                    item.technicianId !== technicianSelected.technicianId
                )
            );

            clearTechnicianSelected();
            setModalDelete(false);
            toast.success("Técnico deletado com sucesso!");
        } catch (error) {
            console.log(error);
            toast.error("Erro ao deletar técnico!");
        } finally {
            setIsLoading(false);
        }
    }
    useEffect(() => {
        getTechnician();
    }, []);

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
                            setModalAdd(true);
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
                                setModalAdd(true);
                            }}
                        />
                    </div>
                    <ul>
                        {technician.map(value => (
                            <TechnicianList
                                key={value.technicianId}
                                technician={value}
                                onEdit={() => {
                                    setTechnicianSelected(value);
                                    setModalEdit(true);
                                }}
                                onDelete={() => {
                                    setTechnicianSelected(value);
                                    setModalDelete(true)
                                }}
                            />
                        ))}
                    </ul>
                    <CreateTechnician
                        technician={technicianSelected}
                        isOpen={modalAdd}
                        onClose={() => setModalAdd(false)}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={postTechnician}
                        errors={errors}
                    />
                    <EditTechnician
                        technician={technicianSelected}
                        isOpen={modalEdit}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onClose={() => setModalEdit(false)}
                        onSubmit={putTechnician}
                        errors={errors}
                    />
                    <DeleteTechnician
                        technician={technicianSelected}
                        isOpen={modalDelete}
                        onClose={() => setModalDelete(false)}
                        isSubmitting={isSubmitting}
                        onConfirm={deleteTechnician}
                    />
                </section>
            )}

        </Container>

    );
}