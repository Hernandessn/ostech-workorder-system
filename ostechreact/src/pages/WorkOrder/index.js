import { useEffect, useState } from 'react';
import './styles.css';
import api from '../../services/api';
import logoOstech from '../../assets/logo-ostech.png';
import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

export const WorkOrder = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [workOrder, setWorkOrder] = useState([]);

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

    const [modalAdd, setModalAdd] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);


    const handleChange = (e) => {
        const { name, value } = e.target;
        setWorkOrderSelected({
            ...workOrderSelected,
            [name]: value
        });
        console.log(workOrderSelected);
    }

    const getWorkOrder = async () => {
        try {
            const response = await api.get('/workorder');

            console.log(response.data);

            setWorkOrder(response.data);
        } catch (error) {
            console.log(error);

        }
    }

    const postWorkOrder = async () => {
        setIsSubmitting(true);
        try {
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
            setModalAdd(false);
        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    }

    const putWorkOrder = async () => {
        try {
            const response = await api.put(`/workorder/${workOrderSelected.workOrderId}`, workOrderSelected);

            setWorkOrder(prev =>
                prev.map(item =>
                    item.workOrderId == workOrderSelected.workOrderId
                        ? response.data
                        : item
                )
            );

            clearWorkOrderSelected();
            setModalEdit(false);
        } catch (error) {
            console.log(error);
        }
    }

    const deleteWorkOrder = async () => {
        try {
            const response = await api.delete(`/workOrder/${workOrderSelected.workOrderId}`);

            setWorkOrder(prev =>
                prev.filter(item =>
                    item.workOrderId !== workOrderSelected.workOrderId
                )
            );

            clearWorkOrderSelected();
            setModalDelete(false);
        } catch (error) {
            console.log(error);

        }
    }
    useEffect(() => {
        getWorkOrder();
    }, [])

    return (
        <div className='workOrder-container'>
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
            </header>
            <div className='workOrder-item'>
                <h1>WorkOrder List</h1>
                <button type='button' className='btn btn-primary workOrder-button'
                    onClick={() => {
                        clearWorkOrderSelected();
                        setModalAdd(true);
                    }}>
                    <PlusIcon size={22} />
                    Create workOrder
                </button>
            </div>
            <ul className='workOrder-list'>
                {workOrder.map(value => (
                    <li key={value.workOrderId} className='workOrder-item'>
                        <div className='workOrder-info'>
                            <p>{value.title}</p>
                            <p>{value.description}</p>
                            <p>{value.amount}</p>
                            <p>{value.deadline}</p>
                            <p>{value.openingDate}</p>
                            <p>{value.customerId}</p>
                            <p>{value.categoryId}</p>
                            <p>{value.equipmentId}</p>
                        </div>
                        <div className='workOrder-actions' >
                            <button type="button" className="btn-icon btn-edit" onClick={() => {
                                setWorkOrderSelected(value);
                                setModalEdit(true);
                            }}>
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button type="button" className="btn-icon btn-delete" onClick={() => {
                                setWorkOrderSelected(value);
                                setModalDelete(true);
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
                modalClassName="workOrder-modal-fade"
                backdropClassName="workOrder-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalAdd(false)}>Create WorkOrder</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="wo-title">Title:</label>
                        <input
                            id="wo-title"
                            className='form-control'
                            name='title'
                            value={workOrderSelected.title}
                            onChange={handleChange}
                            autoFocus
                        />

                        <label htmlFor="wo-description">Description:</label>
                        <textarea
                            id="wo-description"
                            className='form-control'
                            name='description'
                            rows={3}
                            value={workOrderSelected.description}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-amount">Amount:</label>
                        <input
                            id="wo-amount"
                            type="number"
                            className='form-control'
                            name='amount'
                            value={workOrderSelected.amount}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-deadline">Deadline:</label>
                        <input
                            id="wo-deadline"
                            type="date"
                            className='form-control'
                            name='deadline'
                            value={workOrderSelected.deadline}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-openingDate">Opening Date:</label>
                        <input
                            id="wo-openingDate"
                            type="date"
                            className='form-control'
                            name='openingDate'
                            value={workOrderSelected.openingDate}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-technician">Technician ID:</label>
                        <input
                            id="wo-technician"
                            className='form-control'
                            name='technicianId'
                            value={workOrderSelected.technicianId}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-customerId">Customer ID:</label>
                        <input
                            id="wo-customerId"
                            className='form-control'
                            name='customerId'
                            value={workOrderSelected.customerId}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-categoryId">Category ID:</label>
                        <input
                            id="wo-categoryId"
                            className='form-control'
                            name='categoryId'
                            value={workOrderSelected.categoryId}
                            onChange={handleChange}
                        />

                        <label htmlFor="wo-equipmentId">Equipment ID:</label>
                        <input
                            id="wo-equipmentId"
                            className='form-control'
                            name='equipmentId'
                            value={workOrderSelected.equipmentId}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button
                        className='btn btn-primary'
                        disabled={isSubmitting}
                        onClick={postWorkOrder}
                    >
                        {isSubmitting ? 'Adding...' : 'Add'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        setWorkOrderSelected()
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
                modalClassName="workOrder-modal-fade"
                backdropClassName="workOrder-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalEdit(false)}>Edit WorkOrder</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="edit-wo-id">ID</label>
                        <input
                            id="edit-wo-id"
                            className='form-control'
                            readOnly
                            name='workOrderId'
                            value={workOrderSelected ? workOrderSelected.workOrderId : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-title">Title:</label>
                        <input
                            id="edit-wo-title"
                            className='form-control'
                            name='title'
                            value={workOrderSelected ? workOrderSelected.title : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-description">Description:</label>
                        <textarea
                            id="edit-wo-description"
                            className='form-control'
                            name='description'
                            rows={3}
                            value={workOrderSelected ? workOrderSelected.description : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-amount">Amount:</label>
                        <input
                            id="edit-wo-amount"
                            type="number"
                            className='form-control'
                            name='amount'
                            value={workOrderSelected ? workOrderSelected.amount : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-deadline">Deadline:</label>
                        <input
                            id="edit-wo-deadline"
                            type="date"
                            className='form-control'
                            name='deadline'
                            value={workOrderSelected ? workOrderSelected.deadline : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-openingDate">Opening Date:</label>
                        <input
                            id="edit-wo-openingDate"
                            type="date"
                            className='form-control'
                            name='openingDate'
                            value={workOrderSelected ? workOrderSelected.openingDate : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-status">Status:</label>
                        <input
                            id="edit-wo-status"
                            className='form-control'
                            name='status'
                            value={workOrderSelected ? workOrderSelected.status : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-technician">Technician ID:</label>
                        <input
                            id="edit-wo-technician"
                            className='form-control'
                            name='technicianId'
                            value={workOrderSelected ? workOrderSelected.technician : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-customerId">Customer ID:</label>
                        <input
                            id="edit-wo-customerId"
                            className='form-control'
                            name='customerId'
                            value={workOrderSelected ? workOrderSelected.customerId : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-categoryId">Category ID:</label>
                        <input
                            id="edit-wo-categoryId"
                            className='form-control'
                            name='categoryId'
                            value={workOrderSelected ? workOrderSelected.categoryId : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-wo-equipmentId">Equipment ID:</label>
                        <input
                            id="edit-wo-equipmentId"
                            className='form-control'
                            name='equipmentId'
                            value={workOrderSelected ? workOrderSelected.equipmentId : ''}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={putWorkOrder}>
                        {isSubmitting ? 'Editing...' : 'Edit'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        clearWorkOrderSelected()
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
                modalClassName="workOrder-modal-fade"
                backdropClassName="workOrder-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalDelete(false)}>Delete WorkOrder</ModalHeader>
                <ModalBody>
                    <p>Are you sure you want to delete <strong>{workOrderSelected.title}</strong>?</p>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-danger' onClick={deleteWorkOrder}>
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