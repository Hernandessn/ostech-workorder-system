import { Modal } from '../Modal';

export const CreateWorkOrder = ({
    workOrder,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Create WorkOrder">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-name" className="text-sm font-medium">Name:</label>
                <input
                    id="cat-name"
                    type="text"
                    name="name"
                    value={workOrder.name}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-description" className="text-sm font-medium">Description:</label>
                <input
                    id="cat-description"
                    type="text"
                    name="email"
                    value={workOrder.description}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-amount" className="text-sm font-medium">Amount:</label>
                <input
                    id="cat-amount"
                    type="text"
                    name="name"
                    value={workOrder.amount}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-deadline" className="text-sm font-medium">Deadline:</label>
                <input
                    id="cat-deadline"
                    type="text"
                    name="deadline"
                    value={workOrder.deadline}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-openingDate" className="text-sm font-medium">Opening Date:</label>
                <input
                    id="cat-openingDate"
                    type="text"
                    name="openingDate"
                    value={workOrder.openingDate}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-customerId" className="text-sm font-medium">CustomerId:</label>
                <input
                    id="cat-customerId"
                    type="text"
                    name="customerId"
                    value={workOrder.customerId}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-categoryId" className="text-sm font-medium">CategoryId:</label>
                <input
                    id="cat-categoryId"
                    type="text"
                    name="categoryId"
                    value={workOrder.categoryId}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="cat-equipmentId" className="text-sm font-medium">EquipmentId:</label>
                <input
                    id="cat-equipmentId"
                    type="text"
                    name="equipmentId"
                    value={workOrder.equipmentId}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
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