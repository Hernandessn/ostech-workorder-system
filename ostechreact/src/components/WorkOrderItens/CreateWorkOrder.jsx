import { Modal } from '../Modal';

const selectClass = "rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA] appearance-none";

export const CreateWorkOrder = ({
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
        <Modal isOpen={isOpen} onClose={onClose} title="Create WorkOrder">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-title" className="text-sm font-medium">Title:</label>
                <input
                    id="cat-title"
                    type="text"
                    name="title"
                    value={workOrder.title}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-description" className="text-sm font-medium">Description:</label>
                <input
                    id="cat-description"
                    type="text"
                    name="description"
                    value={workOrder.description}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-amount" className="text-sm font-medium">Amount:</label>
                <input
                    id="cat-amount"
                    type="number"
                    name="amount"
                    value={workOrder.amount}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-deadline" className="text-sm font-medium">Deadline:</label>
                <input
                    id="cat-deadline"
                    type="date"
                    name="deadline"
                    value={workOrder.deadline}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-openingDate" className="text-sm font-medium">Opening Date:</label>
                <input
                    id="cat-openingDate"
                    type="date"
                    name="openingDate"
                    value={workOrder.openingDate}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-customerId" className="text-sm font-medium">Customer:</label>
                <select
                    id="cat-customerId"
                    name="customerId"
                    value={workOrder.customerId}
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

                <label htmlFor="cat-categoryId" className="text-sm font-medium">Category:</label>
                <select
                    id="cat-categoryId"
                    name="categoryId"
                    value={workOrder.categoryId}
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

                <label htmlFor="cat-equipmentId" className="text-sm font-medium">Equipment:</label>
                <select
                    id="cat-equipmentId"
                    name="equipmentId"
                    value={workOrder.equipmentId}
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

                <label htmlFor="cat-technicianId" className="text-sm font-medium">Technician:</label>
                <select
                    id="cat-technicianId"
                    name="technicianId"
                    value={workOrder.technicianId}
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
                    {isSubmitting ? 'Adding...' : 'Add'}
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
};