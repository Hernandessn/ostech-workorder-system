import { Modal } from '../Modal';

const inputClass = (hasError) =>
    `rounded-md bg-[#021526] border px-3 py-2 text-[#E2E2B6] focus:outline-none ${
        hasError ? 'border-red-500 focus:border-red-500' : 'border-[#6EACDA]/40 focus:border-[#6EACDA]'
    }`;

const ErrorText = ({ message }) =>
    message ? <span className="text-red-500 text-sm">{message}</span> : null;

export const EditTechnician = ({
    technician,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit,
    errors
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit Technician">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={technician ? technician.technicianId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-name" className="text-sm font-medium">Name:</label>
                <input
                    id="edit-name"
                    type="text"
                    name="name"
                    value={technician ? technician.name : ''}
                    onChange={onChange}
                    className={inputClass(errors.name)}
                />
                <ErrorText message={errors.name} />

                <label htmlFor="edit-specialty" className="text-sm font-medium">Specialty:</label>
                <input
                    id="edit-specialty"
                    type="text"
                    name="specialty"
                    value={technician ? technician.specialty : ''}
                    onChange={onChange}
                    className={inputClass(errors.specialty)}
                />
                <ErrorText message={errors.specialty} />

                <label htmlFor="edit-contact" className="text-sm font-medium">Contact:</label>
                <input
                    id="edit-contact"
                    type="text"
                    name="contact"
                    value={technician ? technician.contact : ''}
                    onChange={onChange}
                    className={inputClass(errors.contact)}
                />
                <ErrorText message={errors.contact} />

                <label htmlFor="edit-availability" className="text-sm font-medium">Availability:</label>
                <select
                    id="edit-availability"
                    name="availability"
                    value={technician && technician.availability ? 'true' : 'false'}
                    onChange={(e) => onChange({
                        target: { name: 'availability', value: e.target.value }
                    })}
                    className={inputClass(errors.availability)}
                >
                    <option value="true">Available</option>
                    <option value="false">Unavailable</option>
                </select>
                <ErrorText message={errors.availability} />

                <label htmlFor="edit-hiringDate" className="text-sm font-medium">Hiring Date:</label>
                <input
                    id="edit-hiringDate"
                    type="date"
                    name="hiringDate"
                    value={technician ? technician.hiringDate : ''}
                    onChange={onChange}
                    className={inputClass(errors.hiringDate)}
                />
                <ErrorText message={errors.hiringDate} />
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