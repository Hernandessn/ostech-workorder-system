import { Modal } from '../Modal';

export const EditWorkOrder = ({
    workOrder,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit workOrder">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={workOrder ? workOrder.workOrderId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-name" className="text-sm font-medium">Name:</label>
                <input
                    id="edit-name"
                    type="text"
                    name="name"
                    value={workOrder ? workOrder.name : ''}
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
                    type="text"
                    name="amount"
                    value={workOrder ? workOrder.amount : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="edit-deadline" className="text-sm font-medium">Deadline:</label>
                <input
                    id="edit-deadline"
                    type="text"
                    name="deadline"
                    value={workOrder ? workOrder.deadline : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="edit-openingDate" className="text-sm font-medium">Opening Date:</label>
                <input
                    id="edit-openingDate"
                    type="text"
                    name="openingDate"
                    value={workOrder ? workOrder.openingDate : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="edit-customerId" className="text-sm font-medium">Customer ID:</label>
                <input
                    id="edit-customerId"
                    type="text"
                    name="name"
                    value={workOrder ? workOrder.customerId : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="edit-categoryId" className="text-sm font-medium">Category ID:</label>
                <input
                    id="edit-categoryId"
                    type="text"
                    name="name"
                    value={workOrder ? workOrder.categoryId : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="edit-equipmentId" className="text-sm font-medium">Equipment ID:</label>
                <input
                    id="edit-equipmentId"
                    type="text"
                    name="name"
                    value={workOrder ? workOrder.equipmentId : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
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