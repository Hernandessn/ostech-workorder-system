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

export const Equipment = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState(false);
    const [isEmpty, setIsEmpty] = useState([]);

    const [equipmentSelected, setEquipmentSelected] = useState({
        equipmentId: '',
        name: '',
        brand: '',
        model: '',
        serialNumber: ''
    });
    const [equipment, setEquipment] = useState([]);

    const [modalAdd, setModalAdd] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);

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

    const getEquipment = async () => {
        setIsError(false);
        setIsLoading(true);
        try {
            const response = await api.get('/equipment');

            console.log(response.data);
            setEquipment(response.data);
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao carregar a lista!");
        } finally {
            setIsLoading(false);
        }
    }

    const postEquipment = async () => {
        setIsError(false);
        setIsSubmitting(true);
        try {
            const response = await api.post('/equipment', {
                name: equipmentSelected.name,
                brand: equipmentSelected.brand,
                model: equipmentSelected.model,
                serialNumber: equipmentSelected.serialNumber
            });
            setEquipment(prev => [...prev, response.data]);

            clearEquipmentSelected();
            setModalAdd(false);
            toast.success("Equipamento criado com sucesso!");
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao criar equipamento!");
        } finally {
            setIsSubmitting(false);
        }
    }

    const putEquipment = async () => {
        setIsError(false);
        setIsSubmitting(true);
        try {
            const response = await api.put(`/equipment/${equipmentSelected.equipmentId}`, equipmentSelected);

            setEquipment(prev =>
                prev.map(
                    item => item.equipmentId === response.data.equipmentId
                        ? response.data
                        : item
                )
            );

            clearEquipmentSelected();
            setModalEdit(false);
            toast.success("Atualizações salvas com sucesso!");
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao atualizar equipamento!");
        } finally {
            setIsSubmitting(false);
        }
    };


    const deleteEquipment = async () => {
        setIsError(false);
        setIsSubmitting(true);
        try {
            const response = await api.delete(`equipment/${equipmentSelected.equipmentId}`);

            setEquipment(prev =>
                prev.filter(
                    item =>
                        item.equipmentId !== equipmentSelected.equipmentId
                )
            );

            clearEquipmentSelected();
            setModalDelete(false);
            toast.success("Equipamento deletado com sucesso!");
        } catch (error) {
            console.log(error);
            setIsError(true);
            toast.error("Erro ao deletar equipamento");
        } finally {
            setIsSubmitting(false);
        }
    }
    useEffect(() => {
        getEquipment();
    }, []);

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
                            setModalAdd(true)
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
                                setModalAdd(true)
                            }}
                        />
                    </div>
                    <ul className="flex flex-col gap-3">
                        {equipment.map(value => (
                            <EquipmentList
                                key={value.equipmentId}
                                equipment={value}
                                onEdit={() => {
                                    setEquipmentSelected(value)
                                    setModalEdit(true)
                                }}
                                onDelete={() => {
                                    setEquipmentSelected(value)
                                    setModalDelete(true)
                                }}
                            />
                        ))}
                    </ul>
                    <CreateEquipment
                        equipment={equipmentSelected}
                        isOpen={modalAdd}
                        onClose={() => setModalAdd(false)}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={postEquipment}
                    />
                    <EditEquipment
                        equipment={equipmentSelected}
                        isOpen={modalEdit}
                        onClose={() => setModalEdit(false)}
                        isSubmitting={isSubmitting}
                        onChange={handleChange}
                        onSubmit={putEquipment}
                    />
                    <DeleteEquipment
                        equipment={equipmentSelected}
                        isOpen={modalDelete}
                        onClose={() => setModalDelete(false)}
                        isSubmitting={isSubmitting}
                        onConfirm={deleteEquipment}
                    />
                </section>
            )}
        </Container>
    )
}