import { Modal } from '../Modal';

export const EditCategory = ({
    category,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit Category">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={category ? category.categoryId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-name" className="text-sm font-medium">Name:</label>
                <input
                    id="edit-name"
                    type="text"
                    name="name"
                    value={category ? category.name : ''}
                    onChange={onChange}
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />

                <label htmlFor="edit-desc" className="text-sm font-medium">Description:</label>
                <textarea
                    id="edit-desc"
                    name="description"
                    rows={3}
                    value={category ? category.description : ''}
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