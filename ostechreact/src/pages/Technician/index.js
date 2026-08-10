import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';
import { useEffect, useState } from 'react';
import api from '../../services/api';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

import './styles.css';

export const Technician = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

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
        try {
            const response = await api.get('/technician');

            setTechnician(response.data);
        } catch (error) {
            console.log(error);
        }
    }

    const postTechnician = async () => {
        setIsSubmitting(true);
        try {
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
        } catch (error) {
            console.log('STATUS:', error.response?.status);
            console.log('DATA:', error.response?.data);
            console.log('REQUEST:', error.config?.data);
        } finally {
            setIsSubmitting(false);
        }
    }

    const putTechnician = async () => {
        setIsSubmitting(true);
        try {
            const response = await api.put(`/technician/${technicianSelected.technicianId}`, technicianSelected);

            setTechnician(prev =>
                prev.map(item =>
                    item.technicianId === technicianSelected.technicianId
                        ? response.data
                        : item
                )
            );

            clearTechnicianSelected();
            setModalEdit();
        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    }

    const deleteTechnician = async () => {
        try {
            const response = await api.delete(`/technician/${technicianSelected.technicianId}`);

            setTechnician(prev =>
                prev.filter(item =>
                    item.technicianId !== technicianSelected.technicianId
                )
            );

            clearTechnicianSelected();
            setModalDelete(false);
        } catch (error) {
            console.log(error);
        }
    }
    useEffect(() => {
        getTechnician();
    }, []);

    return (
        <div className="technician-container">
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
            </header>
            <div className='technician-item'>
                <h1>Technician List</h1>
                <button type='button' className='btn btn-primary technician-button'
                    onClick={() => {
                        clearTechnicianSelected();
                        setModalAdd(true);
                    }}>
                    <PlusIcon size={22} />
                    Create Technician
                </button>
            </div>
            <ul>
                {technician.map(value => (
                    <li key={value.technicianId} className='technician-item'>
                        <div className='technician-info'>
                            <p>{value.name}</p>
                            <p>{value.specialty}</p>
                            <p>{value.contact}</p>
                            <p>{value.availability}</p>
                            <p>{value.hiringDate}</p>
                        </div>
                        <div className='technician-actions'>
                            <button type="button"
                                className="btn-icon btn-edit"
                                onClick={() => {
                                    setTechnicianSelected(value);
                                    setModalEdit(true);
                                }}>
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button type="button"
                                className="btn-icon btn-delete"
                                onClick={() => {
                                    setTechnicianSelected(value);
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
                centered
                modalClassName="technician-modal-fade"
                backdropClassName="technician-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalAdd(false)}>Create Technician</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="tech-name">Name: </label>
                        <input
                            id="tech-name"
                            type="text"
                            className='form-control'
                            name='name'
                            value={technicianSelected.name}
                            onChange={handleChange}
                            autoFocus
                        />
                        <label htmlFor="tech-specialty">specialty: </label>
                        <input
                            id="tech-specialty"
                            type="text"
                            className='form-control'
                            name='specialty'
                            value={technicianSelected.specialty}
                            onChange={handleChange}
                        />
                        <label htmlFor="tech-contact">Contact: </label>
                        <input
                            id="tech-contact"
                            type="text"
                            className='form-control'
                            name='contact'
                            value={technicianSelected.contact}
                            onChange={handleChange}
                        />
                        <label htmlFor="tech-availability">Availability: </label>
                        <label htmlFor="tech-availability">Availability:</label>

                        <select
                            id="tech-availability"
                            className="form-control"
                            name="availability"
                            value={technicianSelected.availability}
                            onChange={handleChange}
                        >
                            <option value="">Select...</option>
                            <option value="true">Available</option>
                            <option value="false">Unavailable</option>
                        </select>
                        <label htmlFor="tech-hiringDate">Hiring Date: </label>
                        <input
                            id="tech-hiringDate"
                            type="date"
                            className='form-control'
                            name='hiringDate'
                            value={technicianSelected.hiringDate}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button
                        className='btn btn-primary'
                        onClick={postTechnician}
                        disabled={isSubmitting}>
                        {isSubmitting ? 'Adding...' : 'Add'}
                    </button>
                    <button
                        className='btn btn-danger'
                        onClick={() => {
                            clearTechnicianSelected();
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
                modalClassName="technician-modal-fade"
                backdropClassName="technician-modal-backdrop">
                <ModalHeader toggle={() => setModalEdit(false)}>Edit Technician</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label>ID</label>
                        <input
                            id='edit-cust-id'
                            className='form-control'
                            readOnly
                            name='technicianId'
                            value={technicianSelected ? technicianSelected.technicianId : ''}
                            onChange={handleChange}
                        />
                        <label>Name</label>
                        <input
                            id='edit-cust-name'
                            className='form-control'
                            name='name'
                            value={technicianSelected ? technicianSelected.name : ''}
                            onChange={handleChange}
                        />
                        <label>specialty</label>
                        <input
                            id='edit-cust-specialty'
                            className='form-control'
                            name='specialty'
                            value={technicianSelected ? technicianSelected.specialty : ''}
                            onChange={handleChange}
                        />
                        <label>Contact</label>
                        <input
                            id='edit-cust-contact'
                            className='form-control'
                            name='contact'
                            value={technicianSelected ? technicianSelected.contact : ''}
                            onChange={handleChange}
                        />
                        <label>Availability</label>
                        <input
                            id='edit-cust-availability'
                            className='form-control'
                            name='availability'
                            value={technicianSelected ? technicianSelected.availability : ''}
                            onChange={handleChange}
                        />
                        <label>Hiring Date</label>
                        <input
                            id='edit-cust-hiringDate'
                            className='form-control'
                            name='hiringDate'
                            value={technicianSelected ? technicianSelected.hiringDate : ''}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={putTechnician}>
                        {isSubmitting ? 'Editing...' : 'Edit'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        clearTechnicianSelected()
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
                modalClassName="technician-modal-fade"
                backdropClassName="technician-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalDelete(false)}>Delete technician</ModalHeader>
                <ModalBody>
                    <p>Are you sure you want to delete <strong>{technicianSelected.name}</strong>?</p>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-danger' onClick={deleteTechnician}>
                        Yes
                    </button>
                    <button className='btn btn-secondary' onClick={() => setModalDelete(false)}>
                        No
                    </button>
                </ModalFooter>
            </Modal>
        </div>
    );
}