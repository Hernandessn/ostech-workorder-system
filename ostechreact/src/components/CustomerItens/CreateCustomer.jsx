import { Modal } from '../Modal';

export const CreateCustomer = ({
    customer,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Create customer">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-name" className="text-sm font-medium">Name:</label>
                <input
                    id="cat-name"
                    type="text"
                    name="name"
                    value={customer.name}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-email" className="text-sm font-medium">Email:</label>
                <input
                    id="cat-email"
                    type="text"
                    name="email"
                    value={customer.email}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-phone" className="text-sm font-medium">Phone:</label>
                <input
                    id="cat-phone"
                    type="text"
                    name="phone"
                    value={customer.phone}
                    onChange={onChange}
                    autoFocus
                    className="rounded-md bg-[#021526] border border-[#6EACDA]/40 px-3 py-2 text-[#E2E2B6] focus:outline-none focus:border-[#6EACDA]"
                />
                <label htmlFor="cat-document" className="text-sm font-medium">Document:</label>
                <input
                    id="cat-document"
                    type="text"
                    name="document"
                    value={customer.document}
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