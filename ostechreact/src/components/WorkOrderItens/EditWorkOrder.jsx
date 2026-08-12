import { Modal } from '../Modal';

const selectClass = "rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA] appearance-none";

export const EditWorkOrder = ({
    workOrder,
    customers,
    technicians,
    categories,
    equipments,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit WorkOrder">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={workOrder ? workOrder.workOrderId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-title" className="text-sm font-medium">Title:</label>
                <input
                    id="edit-title"
                    type="text"
                    name="title"
                    value={workOrder ? workOrder.title : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-description" className="text-sm font-medium">Description:</label>
                <input
                    id="edit-description"
                    type="text"
                    name="description"
                    value={workOrder ? workOrder.description : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-amount" className="text-sm font-medium">Amount:</label>
                <input
                    id="edit-amount"
                    type="number"
                    name="amount"
                    value={workOrder ? workOrder.amount : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-deadline" className="text-sm font-medium">Deadline:</label>
                <input
                    id="edit-deadline"
                    type="date"
                    name="deadline"
                    value={workOrder ? workOrder.deadline : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-openingDate" className="text-sm font-medium">Opening Date:</label>
                <input
                    id="edit-openingDate"
                    type="date"
                    name="openingDate"
                    value={workOrder ? workOrder.openingDate : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-customerId" className="text-sm font-medium">Customer:</label>
                <select
                    id="edit-customerId"
                    name="customerId"
                    value={workOrder ? workOrder.customerId : ''}
                    onChange={onChange}
                    className={selectClass}
                >
                    <option value="">Select a customer</option>
                    {customers.map(customer => (
                        <option key={customer.customerId} value={customer.customerId}>
                            {customer.name}
                        </option>
                    ))}
                </select>

                <label htmlFor="edit-categoryId" className="text-sm font-medium">Category:</label>
                <select
                    id="edit-categoryId"
                    name="categoryId"
                    value={workOrder ? workOrder.categoryId : ''}
                    onChange={onChange}
                    className={selectClass}
                >
                    <option value="">Select a category</option>
                    {categories.map(category => (
                        <option key={category.categoryId} value={category.categoryId}>
                            {category.name}
                        </option>
                    ))}
                </select>

                <label htmlFor="edit-equipmentId" className="text-sm font-medium">Equipment:</label>
                <select
                    id="edit-equipmentId"
                    name="equipmentId"
                    value={workOrder ? workOrder.equipmentId : ''}
                    onChange={onChange}
                    className={selectClass}
                >
                    <option value="">Select an equipment</option>
                    {equipments.map(equipment => (
                        <option key={equipment.equipmentId} value={equipment.equipmentId}>
                            {equipment.name}
                        </option>
                    ))}
                </select>

                <label htmlFor="edit-technicianId" className="text-sm font-medium">Technician:</label>
                <select
                    id="edit-technicianId"
                    name="technicianId"
                    value={workOrder ? workOrder.technicianId : ''}
                    onChange={onChange}
                    className={selectClass}
                >
                    <option value="">Select a technician</option>
                    {technicians.map(technician => (
                        <option key={technician.technicianId} value={technician.technicianId}>
                            {technician.name}
                        </option>
                    ))}
                </select>
            </div>

            <div className="flex justify-end gap-2 mt-4">
                <button
                    onClick={onSubmit}
                    disabled={isSubmitting}
                    className="px-4 py-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-[#6EACDA] hover:text-[#021526] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                    {isSubmitting ? 'Editing...' : 'Edit'}
                </button>
                <button
                    onClick={onClose}
                    className="px-4 py-2 rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors"
                >
                    Cancel
                </button>
            </div>
        </Modal>
    );
}