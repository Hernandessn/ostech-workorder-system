import { useState, useEffect } from 'react';
import { PencilSimpleIcon, PlusIcon, TrashIcon } from '@phosphor-icons/react';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import './styles.css'
import api from '../../services/api';
import logoOstech from '../../assets/logo-ostech.png';

export const Category = () => {
    const [isSubmitting, setIsSubmitting] = useState(false);

    const [categorySelected, setCategorySelected] = useState({
        categoryId: '',
        name: '',
        description: ''
    });

    const [category, setCategory] = useState([]);
    const [modalAdd, setModalAdd] = useState(false);
    const [modalDelete, setModalDelete] = useState(false);
    const [modalEdit, setModalEdit] = useState(false);

    // Abrir ou fechar modal para adicionar
    const openCloseAddModal = () => {
        setModalAdd(!modalAdd);
    }
    const openCloseDeleteModal = () => {
        setModalDelete(!modalDelete);
    }

    const openCloseEditModal = () => {
        setModalEdit(!modalEdit);
    }

    const handleChange = e => {
        const { name, value } = e.target;
        setCategorySelected({
            ...categorySelected,
            [name]: value
        });
        console.log(categorySelected);
    };

    const getCategory = async () => {
        try {
            const response = await api.get('/category');
            console.log(response.data);
            setCategory(response.data);
        } catch (error) {
            console.log(error)
        }
    };

    const postCategory = async () => {
        try {
            setIsSubmitting(true);

            const response = await api.post('/category', {
                name: categorySelected.name,
                description: categorySelected.description
            });

            setCategory(prev => [...prev, response.data]);

            setCategorySelected({
                categoryId: '',
                name: '',
                description: ''
            });

            setModalAdd(false);

        } catch (error) {
            console.log(error);
        } finally {
            setIsSubmitting(false);
        }
    };

    const putCategory = async () => {
        try {
            const response = await api.put(
                `/category/${categorySelected.categoryId}`,
                categorySelected
            );

            setCategory(prev =>
                prev.map(item =>
                    item.categoryId === response.data.categoryId
                        ? response.data
                        : item
                )
            );

            setCategorySelected({
                categoryId: '',
                name: '',
                description: ''
            });

            setModalEdit(false);

        } catch (error) {
            console.log(error);
        }
    };
    const deleteCategory = async () => {
        try {
            await api.delete(
                `/category/${categorySelected.categoryId}`
            );

            setCategory(prev =>
                prev.filter(
                    item =>
                        item.categoryId !== categorySelected.categoryId
                )
            );

            setCategorySelected({
                categoryId: '',
                name: '',
                description: ''
            });

            setModalDelete(false);

        } catch (error) {
            console.log(error);
        }
    };
    useEffect(() => {
        getCategory();
    }, []);



    return (
        <div className="category-container">
            <header>
                <img src={logoOstech} alt='Logo OSTech' />
                <h2>Bem vindo!<strong> Hernandes</strong>!</h2>
            </header>
            <div className='category-item'>
                <h1>Category List</h1>
                <button type='button' className='btn btn-primary category-button' onClick={() => setModalAdd(true)}>
                    <PlusIcon size={22} />
                    Create Category
                </button>
            </div>
            <ul className="category-list">
                {category.map(value => (
                    <li key={value.categoryId} className="category-item">
                        <div className="category-info">
                            <p className="category-name">{value.name}</p>
                            <p className="category-description">{value.description}</p>
                        </div>
                        <div className="category-actions">
                            <button type="button" className="btn-icon btn-edit"
                                onClick={() => {
                                    setCategorySelected(value)
                                    setModalEdit(true)
                                }}>
                                <PencilSimpleIcon size={22} />
                            </button>
                            <button
                                type="button"
                                className="btn-icon btn-delete"
                                onClick={() => {
                                    setCategorySelected(value)
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
                modalClassName="category-modal-fade"
                backdropClassName="category-modal-backdrop"
            >
                <ModalHeader toggle={openCloseAddModal}>Create category</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="cat-name">Name: </label>
                        <input
                            id="cat-name"
                            type='text'
                            className='form-control'
                            name='name'
                            value={categorySelected.name}
                            onChange={handleChange}
                            autoFocus
                        />

                        <label htmlFor="cat-desc">Description: </label>
                        <textarea
                            id="cat-desc"
                            className='form-control'
                            name='description'
                            rows={3}
                            value={categorySelected.description}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button
                        className='btn btn-primary'
                        onClick={postCategory}
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Adding...' : 'Add'}
                    </button>
                    <button className='btn btn-danger' onClick={() => setModalAdd(false)}>
                        Cancel
                    </button>
                </ModalFooter>
            </Modal>
            <Modal
                isOpen={modalEdit}
                toggle={() => setModalEdit(false)}
                centered
                modalClassName="category-modal-fade"
                backdropClassName="category-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalEdit(false)}>Edit Category</ModalHeader>
                <ModalBody>
                    <div className='form-group'>
                        <label htmlFor="edit-id">ID</label>
                        <input
                            id="edit-id"
                            type='text'
                            className='form-control'
                            readOnly
                            value={categorySelected ? categorySelected.categoryId : ''}
                        />

                        <label htmlFor="edit-name">Name: </label>
                        <input
                            id="edit-name"
                            type='text'
                            className='form-control'
                            name='name'
                            value={categorySelected ? categorySelected.name : ''}
                            onChange={handleChange}
                        />

                        <label htmlFor="edit-desc">Description: </label>
                        <textarea
                            id="edit-desc"
                            className='form-control'
                            name='description'
                            rows={3}
                            value={categorySelected ? categorySelected.description : ''}
                            onChange={handleChange}
                        />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-primary' onClick={putCategory}>
                        {isSubmitting ? 'Editing...' : 'Edit'}
                    </button>
                    <button className='btn btn-danger' onClick={() => setModalEdit(false)}>
                        Cancel
                    </button>
                </ModalFooter>
            </Modal>

            <Modal
                isOpen={modalDelete}
                toggle={() => setModalDelete(false)}
                centered
                modalClassName="category-modal-fade"
                backdropClassName="category-modal-backdrop"
            >
                <ModalHeader toggle={() => setModalDelete(false)}>Delete category</ModalHeader>
                <ModalBody>
                    <p>Are you sure you want to delete <strong>{categorySelected.name}</strong>?</p>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-danger' onClick={deleteCategory}>
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