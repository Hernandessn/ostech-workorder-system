import { Modal } from '../Modal';

const inputClass = (hasError) =>
    `rounded-md bg-[#021526] border px-3 py-2 text-[#E2E2B6] focus:outline-none ${
        hasError ? 'border-red-500 focus:border-red-500' : 'border-[#6EACDA]/40 focus:border-[#6EACDA]'
    }`;

const ErrorText = ({ message }) =>
    message ? <span className="text-red-500 text-sm">{message}</span> : null;

export const EditCustomer = ({
    customer,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit,
    errors
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit Customer">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={customer ? customer.customerId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-name" className="text-sm font-medium">Name:</label>
                <input
                    id="edit-name"
                    type="text"
                    name="name"
                    value={customer ? customer.name : ''}
                    onChange={onChange}
                    className={inputClass(errors.name)}
                />
                <ErrorText message={errors.name} />

                <label htmlFor="edit-email" className="text-sm font-medium">Email:</label>
                <input
                    id="edit-email"
                    type="email"
                    name="email"
                    value={customer ? customer.email : ''}
                    onChange={onChange}
                    className={inputClass(errors.email)}
                />
                <ErrorText message={errors.email} />

                <label htmlFor="edit-phone" className="text-sm font-medium">Phone:</label>
                <input
                    id="edit-phone"
                    type="text"
                    name="phone"
                    value={customer ? customer.phone : ''}
                    onChange={onChange}
                    className={inputClass(errors.phone)}
                />
                <ErrorText message={errors.phone} />

                <label htmlFor="edit-document" className="text-sm font-medium">Document:</label>
                <input
                    id="edit-document"
                    type="text"
                    name="document"
                    value={customer ? customer.document : ''}
                    onChange={onChange}
                    className={inputClass(errors.document)}
                />
                <ErrorText message={errors.document} />
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