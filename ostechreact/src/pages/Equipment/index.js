import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';
import './styles.css';
import api from '../../services/api';
import { useEffect, useState } from 'react';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

export const Equipment = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

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
        try {
            const response = await api.get('/equipment');

            console.log(response.data);
            setEquipment(response.data);
        } catch (error) {
            console.log(error);
        }
    }

    const postEquipment = async () => {
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
        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    }

    const putEquipment = async () => {
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

        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    };


    const deleteEquipment = async () => {
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
        } catch (error) {
            console.log(error);
        }
    }
    useEffect(() => {
        getEquipment();
    }, []);

    return (
        <div className="equipment-container">
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
                <h2>Bem vindo!<strong> Hernandes</strong>!</h2>
            </header>
            <div className='equipment-item'>
                <h1>Equipment List</h1>
                <button type='button' className='btn btn-primary equipment-button'
                    onClick={() => {
                        clearEquipmentSelected()
                        setModalAdd(true)
                    }}>
                    <PlusIcon size={22} />
                    Create equipment
                </button>
            </div>
            <ul className='equipment-list'>
                {equipment.map(value => (
                    <li key={value.equipmentId}>
                        <div className='equipment-info'>
                            <p className='equipment-name'>{value.name}</p>
                            <p className='equipment-brand'>{value.brand}</p>
                            <p className='equipment-model'>{value.model}</p>
                            <p className='equipment-serialNumber'>{value.serialNumber}</p>
                        </div>
                        <div className='equipment-actions'>
                            <button type="button" className="btn-icon btn-edit"
                                onClick={() => {
                                    setEquipmentSelected(value)
                                    setModalEdit(true)
                                }}>
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button type="button"
                                className="btn-icon btn-delete"
                                onClick={()=>{
                                    setEquipmentSelected(value)
                                    setModalDelete(true)
                                }}>
                                <TrashIcon size={22} />
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
            <Modal
                isOpen={modalAdd}
                toggle={() => setModalAdd(false)}
                modalClassName="equipment-modal-fade"
                backdropClassName="equipment-modal-backdrop">
                <ModalHeader toggle={() => setModalAdd(false)}>Create equipment</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label>Name: </label>
                        <input
                            id="cust-name"
                            type='text'
                            className='form-control'
                            name='name'
                            value={equipmentSelected.name}
                            onChange={handleChange}
                            autoFocus
                        />
                        <label>Brand: </label>
                        <input
                            id="cust-brand"
                            type='text'
                            className='form-control'
                            name='brand'
                            value={equipmentSelected.brand}
                            onChange={handleChange}
                            autoFocus
                        />
                        <label>Model: </label>
                        <input
                            id="cust-modal"
                            type='text'
                            className='form-control'
                            name='model'
                            value={equipmentSelected.model}
                            onChange={handleChange}
                            autoFocus
                        />
                        <label>Serial Number: </label>
                        <input
                            id="cust-serialNumber"
                            type='text'
                            className='form-control'
                            name='serialNumber'
                            value={equipmentSelected.serialNumber}
                            onChange={handleChange}
                            autoFocus
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button
                        className='btn btn-primary'
                        onClick={postEquipment}
                        disabled={isSubmitting}>
                        {isSubmitting ? 'Adding...' : 'Add'}
                    </button>
                    <button
                        className='btn btn-danger'
                        onClick={() => {
                            clearEquipmentSelected()
                            setModalAdd(false)
                        }}>
                        Cancel
                    </button>
                </ModalFooter>
            </Modal>
            <Modal
                isOpen={modalEdit}
                toggle={() => setModalEdit(false)}
                centered
                modalClassName="equipment-modal-fade"
                backdropClassName="equipment-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalEdit(false)}>Edit Equipment</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="edit-eq-id">ID:</label>
                        <input
                            id="edit-eq-id"
                            className='form-control'
                            readOnly
                            name='equipmentId'
                            value={equipmentSelected ? equipmentSelected.equipmentId : ''}
                            onChange={handleChange} />

                        <label htmlFor="edit-eq-name">Name:</label>
                        <input
                            id="edit-eq-name"
                            className='form-control'
                            name='name'
                            value={equipmentSelected ? equipmentSelected.name : ''}
                            onChange={handleChange} />

                        <label htmlFor="edit-eq-brand">Brand:</label>
                        <input
                            id="edit-eq-brand"
                            className='form-control'
                            name='brand'
                            value={equipmentSelected ? equipmentSelected.brand : ''}
                            onChange={handleChange} />

                        <label htmlFor="edit-eq-model">Model:</label>
                        <input
                            id="edit-eq-model"
                            className='form-control'
                            name='model'
                            value={equipmentSelected ? equipmentSelected.model : ''}
                            onChange={handleChange} />

                        <label htmlFor="edit-eq-serial">Serial Number:</label>
                        <input
                            id="edit-eq-serial"
                            className='form-control'
                            name='serialNumber'
                            value={equipmentSelected ? equipmentSelected.serialNumber : ''}
                            onChange={handleChange} />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={putEquipment}>
                        {isSubmitting ? 'Editing...' : 'Edit'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        clearEquipmentSelected()
                        setModalEdit(false)
                    }}>
                        Cancel
                    </button>
                </ModalFooter>
            </Modal>
            <Modal
                isOpen={modalDelete}
                toggle={() => setModalDelete(false)}
                centered
                modalClassName="equipment-modal-fade"
                backdropClassName="equipment-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalDelete(false)}>Delete equipment</ModalHeader>
                <ModalBody>
                    <p>Are you sure you want to delete <strong>{equipmentSelected.name}</strong>?</p>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-danger' onClick={deleteEquipment}>
                        Yes
                    </button>
                    <button className='btn btn-secondary' onClick={() => setModalDelete(false)}>
                        No
                    </button>
                </ModalFooter>
            </Modal>
        </div>
    )
}