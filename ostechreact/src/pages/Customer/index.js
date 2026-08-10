
import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import logoOstech from '../../assets/logo-ostech.png';
import { useEffect, useState } from 'react';
import api from '../../services/api';
import './styles.css';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';


export const Customer = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

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
        try {
            const response = await api.get('/customer');
            console.log(response.data);
            setCustomer(response.data);
        } catch (error) {
            console.log(error);
        }
    }
    const postCustomer = async () => {
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
        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    }

    const putCustomer = async () => {
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

        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    };

    const deleteCustomer = async () => {
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
        } catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        getCustomer();
    }, []);

    return (
        <div className="customer-container">
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
                <h2>Bem vindo!<strong> Hernandes</strong>!</h2>
            </header>
            <div className='customer-item'>
                <h1>Customer List</h1>
                <button type='button' className='btn btn-primary customer-button'
                    onClick={() => {
                        clearCustomerSelected();
                        setModalAdd(true);
                    }}>
                    <PlusIcon size={22} />
                    Create customer
                </button>
            </div>
            <ul className='customer-list'>
                {customer.map(value => (
                    <li key={value.customerId} className='customer-item'>
                        <div className="customer-info">
                            <p className="customer-name">{value.name}</p>
                            <p className="customer-description">{value.description}</p>
                            <p className="customer-email">{value.email}</p>
                            <p className="customer-phone">{value.phone}</p>
                            <p className="customer-document">{value.document}</p>
                        </div>
                        <div className='customer-actions'>
                            <button type="button" className="btn-icon btn-edit"
                                onClick={() => {
                                    setCustomerSelected(value)
                                    setModalEdit(true)
                                }}>
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button type="button"
                                className="btn-icon btn-delete"
                                onClick={() => {
                                    setCustomerSelected(value)
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
                modalClassName="customer-modal-fade"
                backdropClassName="customer-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalAdd(false)}>Create customer</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="cust-name">Name</label>
                        <input
                            id="cust-name"
                            type='text'
                            className='form-control'
                            name='name'
                            value={customerSelected.name}
                            onChange={handleChange}
                            autoFocus
                        />

                        <label htmlFor="cust-email">Email</label>
                        <input
                            id="cust-email"
                            type='email'
                            className='form-control'
                            name='email'
                            value={customerSelected.email}
                            onChange={handleChange}
                        />

                        <label htmlFor="cust-phone">Phone</label>
                        <input
                            id="cust-phone"
                            type='tel'
                            className='form-control'
                            name='phone'
                            value={customerSelected.phone}
                            onChange={handleChange}
                        />

                        <label htmlFor="cust-document">Document</label>
                        <input
                            id="cust-document"
                            type='text'
                            className='form-control'
                            name='document'
                            value={customerSelected.document}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button
                        className='btn btn-primary'
                        onClick={(postCustomer)}
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Adding...' : 'Add'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        setCustomerSelected();
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
                modalClassName="customer-modal-fade"
                backdropClassName="customer-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalEdit(false)}>Edit Customer</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="edit-cust-id">ID</label>
                        <input
                            id="edit-cust-id"
                            className='form-control'
                            readOnly
                            name='customerId'
                            value={customerSelected ? customerSelected.customerId : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-cust-name">Name: </label>
                        <input
                            id="edit-cust-name"
                            className='form-control'
                            name='name'
                            value={customerSelected ? customerSelected.name : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-cust-email">Email:</label>
                        <input
                            id="edit-cust-email"
                            className='form-control'
                            name='email'
                            value={customerSelected ? customerSelected.email : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-cust-phone">Phone:</label>
                        <input
                            id="edit-cust-phone"
                            className='form-control'
                            name='phone'
                            value={customerSelected ? customerSelected.phone : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-cust-document">Document</label>
                        <input
                            id="edit-cust-document"
                            className='form-control'
                            name='document'
                            value={customerSelected ? customerSelected.document : ''}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={putCustomer}>
                        {isSubmitting ? 'Editing...' : 'Edit'}
                    </button>
                    <button className='btn btn-danger' onClick={() => {
                        clearCustomerSelected()
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
                modalClassName="customer-modal-fade"
                backdropClassName="customer-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalDelete(false)}>Delete customer</ModalHeader>
                <ModalBody>
                    <p>Are you sure you want to delete <strong>{customerSelected.name}</strong>?</p>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-danger' onClick={deleteCustomer}>
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